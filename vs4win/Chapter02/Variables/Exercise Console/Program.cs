using System.Xml;

object height = 1.88; // storing a double in an object 
object name = "Amir"; // storing a string in an object
//Console.WriteLine($"The size of object is {sizeof(Object)}");
Console.WriteLine($"{name} is {height} metres tall.");
//int length1 = name.Length; // gives compile error!
int length2 = ((string)name).Length; // tell compiler it is a string
Console.WriteLine($"{name} has {length2} characters.");

// storing a string in a dynamic object
// string has a Length property
dynamic randomDynamic = "Ahmed";
// int does not have a Length property
//randomDynamic = 12;
// an array of any type has a Length property
randomDynamic = new[] { 3, 5, 7 };

// this compiles but would throw an exception at run-time
// if you later store a data type that does not have a
// property named Length
Console.WriteLine($"Length is {randomDynamic.Length}");

var population = 66_000_000; // 66 million in UK
var weight = 1.88; // in kilograms
var price = 4.99M; // in pounds sterling
var fruit = "Apples"; // strings use double-quotes
var letter = 'Z'; // chars use single-quotes
var happy = true; // Booleans have value of true or false

// good use of var because it avoids the repeated type
// as shown in the more verbose second statement
var xml1 = new XmlDocument();
XmlDocument xml2 = new XmlDocument();
// bad use of var because we cannot tell the type, so we
// should use a specific type declaration as shown in
// the second statement
var file1 = File.CreateText("something1.txt");
StreamWriter file2 = File.CreateText("something2.txt");

Console.WriteLine($"default(int) = {default(int)}");
Console.WriteLine($"default(bool) = {default(bool)}"); //False by default
Console.WriteLine($"default(DateTime) = {default(DateTime)}");
Console.WriteLine($"default(string) = {default(string)}"); //Set to null by default

int number = 13;
Console.WriteLine($"number has been set to: {number}");
number = default;
Console.WriteLine($"number has been reset to its default: {number}");

string[] names; // can reference any size array of strings
//Also initializes a string array of variable names

/*
Note:
Arrays are always of a fixed size at the time of memory allocation, 
so you need to decide how many items you want to store before instantiating them.
*/

// allocating memory for four strings in an array
names = new string[4]; //Search up the new keyword
// storing items at index positions
names[0] = "Kate";
names[1] = "Jack";
names[2] = "Rebecca";
names[3] = "Tom";
// looping through the names
for (int i = 0; i < names.Length; i++)
{
    // output the item at index position i
    Console.WriteLine(names[i]);
}