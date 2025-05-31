﻿namespace WebApplication7.Models.Dtos;

public class PagedResult<T>
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public List<T> Trips { get; set; }
}