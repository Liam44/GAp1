﻿using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace GroupAssignmentpart1
{
    class Vehicle : IEquatable<Vehicle>
    {
        //License plate number
        //Color
        //Brand/model
        //Engine type
        //Amount of Wheels
        //Fuel type
        //Transmition
        //Amount of doors
        //fee
        //Parking spot
        //Parking time

        #region PrivateVariables

        private string registrationPlate;
        private string color;
        private string brand;
        private string model;
        private int numberOfWheels;
        private int numOfDoors;
        private double fee;
        private int parkingSpot;
        private DateTime parkingTime;
        private DateTime checkOutTime;

        #endregion

        #region PublicProperties

        public string RegistrationPlate
        {
            get { return registrationPlate; }
            set { registrationPlate = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public int NumberOfWheels
        {
            get { return numberOfWheels; }
            set { numberOfWheels = value; }
        }

        public int NumberOfDoors
        {
            get { return numOfDoors; }
            set { numOfDoors = value; }
        }

        public double Fee
        {
            get { return fee; }
            set { fee = value; }
        }

        public int ParkingSpot
        {
            get { return parkingSpot; }
            set { parkingSpot = value; }
        }

        public DateTime ParkingTime
        {
            get { return parkingTime; }
            set { parkingTime = value; }
        }

        public DateTime CheckOutTime
        {
            get { return checkOutTime; }
            private set { checkOutTime = value; }
        }

        #endregion

        #region Constructor

        public Vehicle()
        {
        }

        public Vehicle(string registrationPlate,
                       string color,
                       string brand,
                       string model,
                       int numberOfWheels,
                       int numOfDoors,
                       double fee)
        {
            this.registrationPlate = registrationPlate;
            this.color = color;
            this.brand = brand;
            this.model = model;
            this.numberOfWheels = numberOfWheels;
            this.numOfDoors = numOfDoors;
            this.fee = fee;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return string.Join(Constants.MENU_ITEMS_SEPARATOR.ToString(),
                                new string[]{ this.GetType().Name,
                                              registrationPlate.ToUpper(),
                                              brand,
                                              model,
                                              color,
                                              numOfDoors.ToString(),
                                              numberOfWheels.ToString(),
                                              parkingTime.ToString(),
                                              parkingSpot.ToString() });
        }

        public bool Equals(Vehicle other)
        {
            if (other == null) return false;
            return (this.registrationPlate.Equals(other.registrationPlate));
        }

        public double PayCheckOut()
        {
            checkOutTime = DateTime.Now;

            return (checkOutTime - parkingTime).TotalMinutes * fee;
        }

        #endregion

        #region Serialization management

        /// <summary>
        /// Creates a new branch to the XML tree, containing all informations related to the vehicle
        /// </summary>
        /// <returns></returns>
        public XElement Serialize()
        {
            XElement element = new XElement("Vehicle",
                new XElement("LicencePlate", registrationPlate),
                new XElement("Color", color),
                new XElement("Brand", brand),
                new XElement("Model", model),
                new XElement("NumberOfWheels", numberOfWheels.ToString()),
                new XElement("NumberOfDoors", numOfDoors.ToString()),
                new XElement("Fee", fee.ToString()),
                new XElement("ParkingSpot", parkingSpot.ToString()),
                new XElement("ParkingTime",
                    new XElement("Year", parkingTime.Year),
                    new XElement("Month", parkingTime.Month),
                    new XElement("Day", parkingTime.Day),
                    new XElement("Hours", parkingTime.Hour),
                    new XElement("Minutes", parkingTime.Minute),
                    new XElement("Seconds", parkingTime.Second)));

            return element;
        }

        /// <summary>
        /// Parses and extracts the information about the vehicle
        /// </summary>
        /// <param name="element">Branch from the XML tree where the informations are stored</param>
        /// <returns>Indicates if the deserialization went fine or not</returns>
        public bool Deserialize(XElement element)
        {
            try
            {
                registrationPlate = (string)element.Element("LicencePlate");
                color = (string)element.Element("Color");
                brand = (string)element.Element("Brand");
                model = (string)element.Element("Model");
                numberOfWheels = 0;
                int.TryParse((string)element.Element("NumberOfWheels"), out numberOfWheels);
                numOfDoors = 0;
                int.TryParse((string)element.Element("NumberOfDoors"), out numOfDoors);
                fee = (double)element.Element("Fee");
                parkingSpot = -1;
                int.TryParse((string)element.Element("ParkingSpot"), out parkingSpot);
                element = element.Element("ParkingTime");
                parkingTime = new DateTime(int.Parse((string)element.Element("Year")),
                                           int.Parse((string)element.Element("Month")),
                                           int.Parse((string)element.Element("Day")),
                                           int.Parse((string)element.Element("Hours")),
                                           int.Parse((string)element.Element("Minutes")),
                                           int.Parse((string)element.Element("Seconds")));

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
