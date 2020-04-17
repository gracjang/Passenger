using System;

namespace Passenger.Core.Domain
{
  public class Vehicle
  {
    public string Name { get; protected set; }
    public int Seats { get; protected set; }
    public string Brand { get; protected set; }

    protected Vehicle()
    {
    }

    protected Vehicle(string name, int seats, string brand)
    {
      SetName(name);
      SetSeats(seats);
      SetBrand(brand);
    }

    public static void Create(string name, int seats, string brand)
      => new Vehicle(name, seats, brand);

    private void SetName(string name)
    {
      if(string.IsNullOrEmpty(name))
      {
        throw new Exception("Name can't be empty");
      }

      if(Name == name)
      {
        return;
      }

      Name = name;
    }

    private void SetSeats(int seats)
    {
      if(seats < 0)
      {
        throw new Exception("Seats must be greather than 0.");
      }

      if(seats > 9)
      {
        throw new Exception("Seats must be lower than 9.");
      }

      if(Seats == seats)
      {
        return;
      }

      Seats = seats;
    }

    private void SetBrand(string brand)
    {
      if(string.IsNullOrWhiteSpace(brand))
      {
        throw new Exception("Brand can't be null");
      }

      if(Brand == brand)
      {
        return;
      }

      Brand = brand;
    }
  }
}