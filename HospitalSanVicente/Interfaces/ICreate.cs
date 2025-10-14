namespace HospitalSanVicente.Interfaces;

public interface ICreate<T>
{
    T Register(T person);
}