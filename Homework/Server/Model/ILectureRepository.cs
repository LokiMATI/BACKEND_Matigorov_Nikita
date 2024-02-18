namespace Homework;

public interface ILectureRepository
{
    List<GALecture> GetAllLectures();
    void AddLecture(Lecture lecture);
    void DeleteLecture(int id);
}
