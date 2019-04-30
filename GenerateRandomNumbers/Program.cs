using System;
using System.Collections;

namespace GenerateRandomNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList list = new ArrayList();//声明一个集合对象

            Random r = new Random();//声明一个随机对象
            for (int i = 0; i < 10; i++)
            {
                int number = r.Next(0, 1000);//生成一个随机数，0-9
                while (list.Contains(number))//判断集合中有没有生成的随机数，如果有，则重新生成一个随机数，直到生成的随机数list集合中没有才退出循环
                {
                    number = r.Next(0, 1000);
                }
                list.Add(number);//将生成的随机数添加到集合对象中
                Console.WriteLine(list[i]);//在控制台中打印出生成的随机数
            }
            Console.ReadKey();          
        }
    }
}
