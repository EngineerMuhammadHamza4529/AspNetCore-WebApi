using System;
using System.Collections.Generic;

namespace project_web_api_crud_.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public string? Address { get; set; }
}
