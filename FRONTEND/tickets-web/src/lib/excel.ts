export interface ExcelColumn<T> {
    header: string
    width: number
    value: (row: T) => string | number | boolean | Date | null
}

// Genera un archivo .xlsx en el navegador y lo descarga.
// exceljs se carga recién al exportar, para no hacer más pesada la carga inicial de la página.
export async function exportToExcel<T>(
    fileName: string,
    sheetName: string,
    columns: ExcelColumn<T>[],
    rows: T[],
) {
    const { Workbook } = await import("exceljs")

    const workbook = new Workbook()
    const sheet = workbook.addWorksheet(sheetName)

    sheet.columns = columns.map((c, i) => ({ header: c.header, key: String(i), width: c.width }))
    sheet.getRow(1).font = { bold: true }
    sheet.views = [{ state: "frozen", ySplit: 1 }] // la fila de títulos queda fija al bajar

    for (const row of rows) {
        sheet.addRow(Object.fromEntries(columns.map((c, i) => [String(i), c.value(row)])))
    }

    const buffer = await workbook.xlsx.writeBuffer()
    const blob = new Blob([buffer], {
        type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
    })

    const url = URL.createObjectURL(blob)
    const link = document.createElement("a")
    link.href = url
    link.download = `${fileName}.xlsx`
    link.click()
    URL.revokeObjectURL(url)
}
