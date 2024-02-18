namespace Homework;

using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.IO; 
using System.Net.Http;
using System.Text; 
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
public class LecturesAPI : ControllerBase
{
    private readonly ILectureRepository lectureRepository;

    public LecturesAPI(ILectureRepository _lectureRepository)
    {
        lectureRepository = _lectureRepository;
    }

    [HttpPost]
    [Route("/lectures/add")]
    public IActionResult Add([FromBody] Lecture newLecture)
    { 
        lectureRepository.AddLecture(newLecture);
        return Ok(lectureRepository.GetAllLectures());
    }

    [HttpPost]
    [Route("/lectures/delete")]
    public IActionResult Delete(int id)
        {
            lectureRepository.DeleteLecture(id);
            return Ok($"{id} удален");
        }

    [HttpGet]
    [Route("/lectures/show")]
    public IActionResult Show()
    {
        return Ok(lectureRepository.GetAllLectures());
    }    
}
