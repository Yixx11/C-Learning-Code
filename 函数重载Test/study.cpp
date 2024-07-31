#include<iostream>
using namespace std;

void print_f(const int &p) 
{
	//p += 20;
	cout << "value is"<< p << endl;
}

void func(int &a) 
{
	cout << " func(int &a)调用" << endl;
}

void func(const int& a)
{
	cout << "func(const int &a)调用" << endl;
}

void func2(int a ,int b = 10) 
{
	cout << a + b << endl;
}
int main()  
{
	const int val = 100;
	//print_f(val);
	func(val);
	//cout << "val is" << val << endl;
	func2(30);
	system("pause");
}