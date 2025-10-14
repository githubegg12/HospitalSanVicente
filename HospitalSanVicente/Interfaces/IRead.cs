namespace HospitalSanVicente.Interfaces;

public interface IRead<T>
{
    T? GetById(string id);
    List<T> GetAll();
}