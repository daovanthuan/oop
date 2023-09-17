using System;
using System.Text;

public class Person
{
    public string Name { get; set; }
    public string Address { get; set; }
    public double Salary { get; set; }

    public Person(string name, string address, double salary)
    {
        Name = name;
        Address = address;
        Salary = salary;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Person[] persons = new Person[3];

        for (int i = 0; i < persons.Length; i++)
        {
            Console.WriteLine("Nhập Thông Tin");

            string name = InputString("Vui Lòng Nhập Tên: ");
            string address = InputString("Vui Lòng Nhập Địa Chỉ: ");
            double salary = InputSalary("Vui Lòng Nhập Lương: ");

            Person person = new Person(name, address, salary);
            persons[i] = person;
        }

        Console.WriteLine("\nThông Tin Được Sắp Xếp Theo Mức Lương:\n");
        persons = SortBySalary(persons);

        foreach (Person person in persons)
        {
            DisplayPersonInfo(person);
            Console.WriteLine();
        }
    }

    private static string InputString(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    private static double InputSalary(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine();

            if (double.TryParse(input, out double salary)) //double.TryParse() kiểm tra xem phương thức có hợp lệ
            {
                if (salary >= 0)
                {
                    return salary;
                }
                else
                {
                    Console.WriteLine("Vui Lòng Nhập Mức Lương Lớn Hơn 0.");
                }
            }
            else
            {
                Console.WriteLine("Bạn phải nhập một số hợp lệ cho tiền lương.");
            }
        }
    }

    private static void DisplayPersonInfo(Person person)
    {
        Console.WriteLine("Thông tin của người bạn đã nhập:");
        Console.WriteLine($"Tên: {person.Name}");
        Console.WriteLine($"Địa Chỉ: {person.Address}");
        Console.WriteLine($"Lương: {person.Salary}");
    }

    private static Person[] SortBySalary(Person[] persons)
    {
        try
        {
            for (int i = 0; i < persons.Length - 1; i++)
            {
                for (int j = 0; j < persons.Length - i - 1; j++)
                {
                    if (persons[j].Salary > persons[j + 1].Salary)
                    {
                        Person temp = persons[j];
                        persons[j] = persons[j + 1];
                        persons[j + 1] = temp;
                    }
                }
            }

            return persons;
        }
        catch (Exception)
        {
            throw new Exception("Không Thể Sắp Xếp.");
        }
    }
}