import { useContext } from "react";
import { MenuContext  } from "./menuContext";

export function useMenu(){
    const value = useContext(MenuContext)
    if(!value) throw new Error("useMenu dabe usarse dentro de <MenuPro>.")
    
    return value
}