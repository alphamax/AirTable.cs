# AirTable.cs

C# library for accessing [AirTable](https://airtable.com/) API. A portable class library if you want to use it on multiple platforms. It is based on Newtonsoft JSON reader.
Everything is object oriented : Get a **Base** with your secret key, the **Base Id** & the **Base Name**. Act on this Base.

You can find two project. One is the API & the other is a test program. You need to fill your secret API key, the Base name & Id to make it work. The main part is located [in the Program.cs](https://github.com/alphamax/AirTable.cs/blob/master/Sources/AirTable/AirTable.Test/Program.cs).
It relies on [Agile Template](https://airtable.com/templates/featured/expJAKb5VbrjX1RkC/agile-product-planning).

First commit is few hours of work. Be gentle & open issues if there is any problems :blush:

## What library handle :
* List with parameters (formula is a string but everything else is object oriented)
* Create lines
* Update lines based on data changed by the user
* Delete lines

## What library do not handle : 
* Pagination & attachment

## To be tested :
* Different esoteric characters to test/challenge encoding !

>NB : There is a wink in the code :beer:
