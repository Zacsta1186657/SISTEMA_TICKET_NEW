using System;
using System.Collections.Generic;
using System.Text;

namespace TicketsApi.Application.Auth;

public record MenuItemDto(int MenuId, int? ParentMenuId, string Code, string Name, string? Url, string? Icon, int SortOrder);

public record MenuNode(int MenuId, string Code, string Name, string? Url, string? Icon, int SortOrder, List<MenuNode> Children); 

