namespace Homework;

using System;
using System.ComponentModel.DataAnnotations;

public class Lecture
{
    public string Theme { get; set; }
    public string Date { get; set; }
    public string Text { get; set; }
    public Lecture(string theme, string text, string date)
    {
        Theme = theme;
        Text = text;
        Date = date;
    }
}