/*
var numString = "10.5";
int num = int.Parse(numString);
Console.WriteLine(num);

//int.Parse는 int, 즉 정수만 파싱이 가능하기 때문에
//실수형태(float)값인 10.5를 파싱하지 못하고 format exception이 발생함
*/
var numString = "10.5";
int num;

try
{
    num = int.Parse(numString);
}
catch (FormatException)
{
    num = 0;
}

Console.WriteLine(num);