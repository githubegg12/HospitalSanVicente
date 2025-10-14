namespace HospitalSanVicente.Models;

// Abstract base class representing a person
public abstract class Person
{
    
    private Guid _id;
    private string _firstName;
    private string _lastName;
    private int _documentID;
    private string _phoneNumber;
    private string _email;


    // Constructor
    public Person(string firstName, string lastName,int documentID, string phoneNumber, string email )
    {
        Id = Guid.NewGuid(); // Unique ID generated on creation
        FirstName = firstName;
        LastName = lastName;
        DocumentID = documentID; 
        PhoneNumber = phoneNumber;
        Email = email;
       
    }
    public Guid Id
    {
        get => _id;
        private set => _id = value;
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                _lastName = value;
        }
    }
    
    public int DocumentID
    {
        get => _documentID;
        set
        {
            if (value > 0) // Only accept positive integers
                _documentID = value;
        }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                _phoneNumber = value;
        }
    }
    public string Email
    {
        get => _email;
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Contains("@"))
                _email = value;
        }
    }


}