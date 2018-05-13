## Sharp Column Indenter - Visual Studio Extension
A Smart source code indenter that indent the code into columns. Also known as "code alignment".
[Download](https://marketplace.visualstudio.com/items?itemName=kudchikarsk.sharp-column-indenter)

## Contents

*   Introduction
*   Examples Of Improvements In Code Readability Using Code Alignment
*   Steps To Use Sharp Column Indenter

## Introduction

Tthe most important aspect of programming is the readability of the source code that you write or maintain. This involves many things, from the syntax of the programming language, to the variable names, comments, and indentation.

Here indention is something where Sharp Column Indenter can help you with.

## What Is Column Indention/ Code Alignment?

In mathematics you always keep your equals lined up directly underneath the one above. It keeps it clean and lets you know you're working on the same problem, for example:
            
            y   = 2x
            y/2 = x
            
some code here Programming is slightly different. We often have a lot of assignments underneath each other, and while they are not strictly the same as maths, there is a close relationship. As such, aligning the equals allows us to quickly spot the relationship.

Further, it makes your code so much more readable. Without alignment, the code is like opening a CSV file in Notepad. But, if you open the CSV file in Excel, it becomes so much easier to read since the columns have meaning.

## Examples Of Improvements In Code Readability Using Code Alignment


First example

Take a look at this code:

      //natural code with no code alignment and you can see that
      //readability of the code is not that good 
      var people1 = new List<Person>()
      {
          new Person { Name = "Benita", Location = "Bareilly India", Age = 25 },
          new Person { Name = "Deedee Almon Fonsec", Location = "Bari Italy", Age = 32 } ,
          new Person { Name = "Chase Hussain", Location = "Barika Algeria", Age = 45 } ,
          new Person { Name = "Cordia", Location = "Barinas Venezuela", Age = 26 } ,
          new Person { Name = "Malvina Neff", Location = "Barisal Bangladesh", Age = 36 } ,
          new Person { Name = "Erika ", Location = "Barnaul Russia", Age = 56 } ,
          new Person { Name = "Lisabeth Terr", Location = "Barquisimeto Venezuela", Age = 67 } ,
          new Person { Name = "Farrah ", Location = "Barra Mansa Brazil", Age = 57 } ,
          new Person { Name = "Domonique Biv", Location = "Barrackpur India", Age = 57 } ,
          new Person { Name = "Jonah", Location = "Barrancabermeja Colombia", Age = 34 }
      };
      
The idea that I’m talking about is to use something like this below,

      //same above code with column indention
      var people2 = new List<Person>()
      {
          new Person { Name = "Benita"              , Location = "Bareilly India"           , Age = 25 } , 
          new Person { Name = "Deedee Almon Fonsec" , Location = "Bari Italy"               , Age = 32 } , 
          new Person { Name = "Chase Hussain"       , Location = "Barika Algeria"           , Age = 45 } , 
          new Person { Name = "Cordia"              , Location = "Barinas Venezuela"        , Age = 26 } , 
          new Person { Name = "Malvina Neff"        , Location = "Barisal Bangladesh"       , Age = 36 } , 
          new Person { Name = "Erika "              , Location = "Barnaul Russia"           , Age = 56 } , 
          new Person { Name = "Lisabeth Terr"       , Location = "Barquisimeto Venezuela"   , Age = 67 } , 
          new Person { Name = "Farrah "             , Location = "Barra Mansa Brazil"       , Age = 57 } , 
          new Person { Name = "Domonique Biv"       , Location = "Barrackpur India"         , Age = 57 } , 
          new Person { Name = "Jonah"               , Location = "Barrancabermeja Colombia" , Age = 34 }   
      };

The Sharp Column Indenter extension allows you to align by more than just the equals. As you start to see the benefits of alignment, you see that there is so much more to align with:

Compare these:

      var benita = new Person() { Name = "Benita" };
      var deedeeAlmon = new Person() { Name = "Deedee Almon Fonsec" };
      var chaseHussain = new Person() { Name = "Chase Hussain" };
      var cordia = new Person() { Name = "Cordia" };

      benita.Age = 35;
      deedeeAlmon.Age = 12;
      chaseHussain.Age = 24;
      cordia.Age = 22;
      
same code with column indention,
      
      var benita       = new Person ( ) { Name = "Benita"              } ; 
      var deedeeAlmon  = new Person ( ) { Name = "Deedee Almon Fonsec" } ; 
      var chaseHussain = new Person ( ) { Name = "Chase Hussain"       } ; 
      var cordia       = new Person ( ) { Name = "Cordia"              } ; 

      benita       . Age = 35 ; 
      deedeeAlmon  . Age = 12 ; 
      chaseHussain . Age = 24 ; 
      cordia       . Age = 22 ; 

By aligning by the dot we can clearly see that we are setting the same property on each variable, and the thing that changes is the variable name.

This might seem a bit crazy now, but once you start aligning things, it's addictive.

## Steps To Use Sharp Column Indenter

Step 1: Select text you want to align.

![](https://kudchikarsk.github.io/images/01-select-text.jpg "")

Step 2: Select Apply Column Indention command in Edit menu

![](https://kudchikarsk.github.io/images/02-apply-column-indention.jpg "")

Thats it!

I turned Sharp Column Indenter project into a [Github repo](https://github.com/kudchikarsk/sharp-column-indenter) so you can, you know, contribute to it by making pull requests.

If you have constructive criticism, or know of other tools that do the same thing, please leave a comment.
