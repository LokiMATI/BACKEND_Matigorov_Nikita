namespace Homework;

using System;
using System.ComponentModel.DataAnnotations;

public class GALecture
{
    public int Id { get; set; }
    public Lecture _lecture{ get; set; }
    public GALecture(int id, Lecture lecture_copy)
    {
        Id = id;
        _lecture = lecture_copy;
    }
}