using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Application.Auth
{
    public static class MenuTreeBuilder
    {
        public static List<MenuNode> Build(IEnumerable<MenuItemDto> items)
        {
            var list = items.ToList();

            var nodes = list.ToDictionary(
                i => i.MenuId,
                i => new MenuNode(i.MenuId, i.Code, i.Name, i.Url, i.Icon, i.SortOrder, []));

            var roots = new List<MenuNode>();

            foreach (var item in list.OrderBy(i => i.SortOrder))
            {
                var node = nodes[item.MenuId];

                if (item.ParentMenuId is int parentId && nodes.TryGetValue(parentId, out var parent))
                    parent.Children.Add(node); // cuelga de su padre
                else
                    roots.Add(node); // sin padre: es raíz
            }

            return roots;
        }
    }
}
