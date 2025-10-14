namespace HospitalSanVicente.Interfaces;

public interface IUpdate<T>
{
    void Update(string id, T person);
}