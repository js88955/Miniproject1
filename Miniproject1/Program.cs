using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Miniproject1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int function = 0; //int that stores what functions to run
            ArrayList thisClass = new ArrayList(); //Class the students are in
            do //query user at least once
            {   
                //functions
                Console.WriteLine("What would you like to do? Please input the corresponding number.");
                Console.WriteLine("1: Add student to class");
                Console.WriteLine("2: Find student by id");
                Console.WriteLine("3: Change a student's info");
                Console.WriteLine("4: Get the number of students in the class");
                Console.WriteLine("5: Get the age of a student");
                Console.WriteLine("6: Exit");


                string input = Console.ReadLine();
                bool validFunction = int.TryParse(input, out function);

                if (!validFunction || function > 6) //check if user submitted a valid function
                {
                    Console.WriteLine("That is not a valid request.");
                    Console.WriteLine();
                    continue;
                }

                switch (function)
                {
                    case 1: //add a student
                        //student properties
                        int id = 0;
                        string name;
                        DateTime dob;
                        Dictionary<string, int> courses = new Dictionary<string, int>();

                        do
                        {
                            Console.WriteLine("Input Student ID. ID cannot be 0."); //ask for id
                            input = Console.ReadLine();
                            bool validID = int.TryParse(input, out id);

                            if (!validID || id == 0) //check if user submitted a valid id
                            {
                                Console.WriteLine("Not a valid id.");
                                Console.WriteLine();
                                continue;
                            }
                            else
                            {
                                foreach (student s in thisClass) //check if the id is in use
                                {
                                    if (id == s.getID())
                                    {
                                        Console.WriteLine("That id is already used.");
                                        Console.WriteLine();
                                        id = 0;
                                    }
                                }
                            }
                            
                        } while (id == 0);

                        Console.WriteLine("Input Student Name."); //ask for name
                        name = Console.ReadLine();

                        while (true) { 
                            Console.WriteLine("Input Student Date of Birth as MM/DD/YYYY"); //ask for dob
                            input = Console.ReadLine();
                            bool validDob = DateTime.TryParse(input, new CultureInfo("en-US"), DateTimeStyles.None, out dob);
                            if (!validDob) //check if user submitted a valid dob
                            {
                                Console.WriteLine("Not a valid date of birth.");
                                Console.WriteLine();
                                continue;
                            }
                            break;
                        } 

                        while (true) 
                        {
                            Console.WriteLine("What course is the student in? The courses are Math, Science, English, and History."); //ask for course
                            string course = Console.ReadLine();
                            if(!course.Equals("Math") && !course.Equals("Science") 
                                && !course.Equals("English") && !course.Equals("History")) //check if the course is valid
                            {
                                Console.WriteLine("Not a valid course.");
                                Console.WriteLine();
                                course = "invalid";
                            }

                            if (course.Equals("invalid"))
                            {
                                continue;
                            }

                            Console.WriteLine("Input the grade for that course. Grades can be from 0 to 100"); //ask for grade
                            int grade = -1;
                            input = Console.ReadLine();
                            bool validID = int.TryParse(input, out grade);
                            if (!validID || grade < 0 || grade > 100) //check if user submitted a valid grade
                            {
                                Console.WriteLine("Not a valid grade.");
                                Console.WriteLine();
                                continue;
                            }

                            courses.Add(course, grade);
                            break;
                        }

                        student newStudent = new student(id, name, dob, courses);
                        thisClass.Add(newStudent);
                        break;

                    case 2: //print a student's information
                        id = 0;
                        do
                        {
                            Console.WriteLine("Input Student ID. ID cannot be 0."); //ask for id
                            input = Console.ReadLine();
                            bool validID = int.TryParse(input, out id);

                            if (!validID || id == 0) //check if user submitted a valid id
                            {
                                Console.WriteLine("Not a valid id.");
                                Console.WriteLine();
                                continue;
                            }   
                        } while (id == 0);

                        bool found = false;
                        foreach (student s in thisClass) //check if the id is in use
                        {
                            if (id == s.getID())
                            {
                                s.getStudent();
                                found = true;
                                break;
                            }
                        }
                        if (!found) //id not found
                        {
                            Console.WriteLine("Student not found.");
                        }

                        break;

                    case 3: //change student information

                        student toChange = new student();
                        id = 0;
                        do
                        {
                            Console.WriteLine("Input Student ID. ID cannot be 0."); //ask for id
                            input = Console.ReadLine();
                            bool validID = int.TryParse(input, out id);

                            if (!validID || id == 0) //check if user submitted a valid id
                            {
                                Console.WriteLine("Not a valid id.");
                                Console.WriteLine();
                                continue;
                            }
                        } while (id == 0);

                        found = false;

                        foreach (student s in thisClass) //check if the id is in use
                        {
                            if (id == s.getID())
                            {
                                found = true;
                                toChange = s;
                                break;
                            }
                        }
                        if (!found) //id not found
                        {
                            Console.WriteLine("Student not found.");
                            break;
                        }

                        Console.WriteLine("What would you like to change?");
                        Console.WriteLine("1: Name");
                        Console.WriteLine("2: Date of Birth");
                        Console.WriteLine("3: Add a course");
                        Console.WriteLine("4: Remove a course");
                        Console.WriteLine("5: Change a grade");
                        Console.WriteLine("6: Exit");
                        int change;
                        input = Console.ReadLine();
                        bool validChange = int.TryParse(input, out change);

                        if (!validChange || change > 6) //check if user submitted a valid function
                        {
                            Console.WriteLine("That is not a valid request.");
                            Console.WriteLine();
                            continue;
                        }

                        switch (change)
                        {
                            case 1: //change name
                                Console.WriteLine("Input a new name.");
                                toChange.setName(Console.ReadLine());
                                break;

                            case 2: //change date of birth
                                while (true)
                                {
                                    Console.WriteLine("Input Student Date of Birth as MM/DD/YYYY"); //ask for dob
                                    input = Console.ReadLine();
                                    bool validDob = DateTime.TryParse(input, new CultureInfo("en-US"), DateTimeStyles.None, out dob);
                                    if (!validDob) //check if user submitted a valid dob
                                    {
                                        Console.WriteLine("Not a valid date of birth.");
                                        Console.WriteLine();
                                        continue;
                                    }
                                    break;
                                }
                                break;

                            case 3: //add a course
                                Console.WriteLine("What course do you want to add? The courses are Math, Science, English, and History."); //ask for course
                                string course = Console.ReadLine();
                                if (!course.Equals("Math") && !course.Equals("Science")
                                    && !course.Equals("English") && !course.Equals("History")) //check if the course is valid
                                {
                                    Console.WriteLine("Not a valid course.");
                                    Console.WriteLine();
                                    course = "invalid";
                                }

                                if (course.Equals("invalid"))
                                {
                                    continue;
                                }

                                if (toChange.getCourses().ContainsKey(course))
                                {
                                    Console.WriteLine("Student is already in that course.");
                                    continue;
                                }

                                Console.WriteLine("Input the grade for that course. Grades can be from 0 to 100"); //ask for grade
                                int grade = -1;
                                input = Console.ReadLine();
                                bool validID = int.TryParse(input, out grade);
                                if (!validID || grade < 0 || grade > 100) //check if user submitted a valid grade
                                {
                                    Console.WriteLine("Not a valid grade.");
                                    Console.WriteLine();
                                    continue;
                                }

                                toChange.addCourse(course, grade);
                                break;

                            case 4: //remove a course
                                Console.WriteLine("What course do you want to remove? The courses are Math, Science, English, and History."); //ask for course
                                course = Console.ReadLine();
                                if (!course.Equals("Math") && !course.Equals("Science")
                                    && !course.Equals("English") && !course.Equals("History")) //check if the course is valid
                                {
                                    Console.WriteLine("Not a valid course.");
                                    Console.WriteLine();
                                    course = "invalid";
                                }

                                if (course.Equals("invalid"))
                                {
                                    continue;
                                }
                            
                                toChange.removeCourse(course);
                                break;

                            case 5: //change a grade
                                Console.WriteLine("What course's do you want to change? The courses are Math, Science, English, and History."); //ask for course
                                course = Console.ReadLine();
                                if (!course.Equals("Math") && !course.Equals("Science")
                                    && !course.Equals("English") && !course.Equals("History")) //check if the course is valid
                                {
                                    Console.WriteLine("Not a valid course.");
                                    Console.WriteLine();
                                    course = "invalid";
                                }

                                if (course.Equals("invalid"))
                                {
                                    continue;
                                }

                                Console.WriteLine("Input the grade for that course. Grades can be from 0 to 100"); //ask for grade
                                grade = -1;
                                input = Console.ReadLine();
                                validID = int.TryParse(input, out grade);
                                if (!validID || grade < 0 || grade > 100) //check if user submitted a valid grade
                                {
                                    Console.WriteLine("Not a valid grade.");
                                    Console.WriteLine();
                                    continue;
                                }
                                toChange.setGrade(course, grade);
                                break;

                            case 6:
                                break;

                        }


                        break;

                    case 4: //get number of students in this class
                        Console.WriteLine("There are " + thisClass.Count + " students in this class.");
                        break;

                    case 5: //get age of a student
                        id = 0;
                        do
                        {
                            Console.WriteLine("Input Student ID. ID cannot be 0."); //ask for id
                            input = Console.ReadLine();
                            bool validID = int.TryParse(input, out id);

                            if (!validID || id == 0) //check if user submitted a valid id
                            {
                                Console.WriteLine("Not a valid id.");
                                Console.WriteLine();
                                continue;
                            }
                        } while (id == 0);

                        found = false;
                        foreach (student s in thisClass) //check if the id is in use
                        {
                            if (id == s.getID())
                            {
                                Console.WriteLine("That student is " + s.getAge() + " years old.");
                                found = true;
                                break;
                            }
                        }
                        if (!found) //id not found
                        {
                            Console.WriteLine("Student not found.");
                        }
                        break;
                }
            } while (function != 6);
        }   
    }
       
    //Student Class
    public class student{
        private int id;
        private string name;
        private DateTime dob; 
        private Dictionary<string, int> courses; //Hold courses in a dictionary with the course as the keys and grade as the values

        //class constructor
        public student(int id, string name, DateTime dob, Dictionary<string, int> courses)
        {
            this.id = id;
            this.name = name;
            this.dob = dob;
            this.courses = courses;
        }

        //dummy constructor
        public student()
        {

        }
        
        //returns student's id
        public int getID()
        {
            return id;
        }

        //prints the student's information
        public void getStudent()
        {   
            Console.WriteLine("Student Name: " + name);
            Console.WriteLine("Student Date of Birth: " + dob.ToShortDateString());
            foreach(KeyValuePair<string, int> k in courses) //iterate over every key-value pair in the courses dictionary
            {
                Console.WriteLine("Course: {0} Grade: {1}", k.Key, k.Value);
            }
            Console.WriteLine();
        }

        //setters for name and dob
        public void setName(string newName)
        {
            name = newName;
        }

        public void setDob(DateTime newDob)
        {
            dob = newDob;
        }

        //methods for altering the courses
        public Dictionary<string, int> getCourses()
        {
            return courses;
        }

        public void addCourse(string course, int grade)
        {
            courses.Add(course, grade);
        }

        public void removeCourse(string course)
        {
            if (courses.ContainsKey(course))
            {
                courses.Remove(course);
            }
            else
            {
                Console.WriteLine("Student is not taking that course.");
            }
        }

        public void setGrade(string course, int grade)
        {
            if (courses.ContainsKey(course))
            {
                courses[course] = grade;
            }
            else
            {
                Console.WriteLine("Student is not taking that course.");
            }
        }

        //age getter
        public int getAge()
        {
            DateTime today = DateTime.Now;
            int age = today.Year - dob.Year;

            if (today.Month < dob.Month || (today.Month == dob.Month && today.Day < dob.Day)) //check current month or day is before the date of birth's
            {
                age--;
            }

            return age;
        }
    }
}
