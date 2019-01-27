using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffieHellmanProtocol
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Здесь я пытаюсь написать протокол Диффи-Хельмана . \n А за одно поднять скиллы в программировании и криптографии");
            Protocol Program = new Protocol();
            Program.GetG();
        }
        class Protocol
        {
            private int RandomCommonNumberP;
            private int RandomCommonNumberG;
            private double OpenKeyFromA;
            private double OpenKeyFromB;
            private int SecretKeyFromA;
            private int SecretKeyFromB;
            public void GetG()
            {
                start:
                Console.WriteLine("Введите простое число ,которое назовём p:");
                int entering = Convert.ToInt32(Console.ReadLine());
                for(int i=1;i<entering;i++)
                {
                    double quotient0 = (double)entering / i;
                    double quotient = entering / i;
                   // Console.WriteLine("Результат деление с дробным остаткок-"+quotient0+"\n"+quotient+"-результат целочисленного деления");
                    if (quotient==quotient0&& i!=1 )
                    {
                        Console.WriteLine("Вы по всей видимости потерялись в пространстве и ввели не простое число \n Вас откатит обратно");
                        goto start;
                       
                    }                                      
                   // Console.ReadLine();
                }
                RandomCommonNumberP = entering;
                Start:
                Console.WriteLine("Осталось подобрать второе ключевое число g.И тут появляется сложность оно должно соответствовать g^(p-1) % p=1 \n " +
                    "Если вам лениво или вы ничего не поняли ,нажмите y ,что бы компьютер перебрал числа за вас(технологии как никак )");
                string answer = Console.ReadLine();
                int G = 0;
                int g = 2;
                switch (answer)
                {
                    case "y":
                        
                        Console.WriteLine("Начало подбора");
                        int counter = 0;
                        while (
                            g < 1000
                        //Math.Pow(g, entering - 1) % entering != 1
                        //counter <= 2
                        )
                        {
                            //int test;
                            //test = (int)(Math.Pow(g, entering - 1) % entering);
                            //Console.WriteLine("Промежуточное значение " + g);
                            g++;
                            if (Math.Pow(g, entering - 1) % entering == 1)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("Первое число удачно подобрано " +g);
                                G = g;
                                Console.ResetColor();
                                //counter++;
                            }
                            Console.WriteLine((g - 2) + "круг пройден");                           
                        }
                       // Console.WriteLine(g);
                        //Console.WriteLine("При p=17 g должно получиться 2");
                        //Console.WriteLine(Math.Pow(g, entering - 1) % entering == 1);
                        // Console.WriteLine("Проверка для числа p=71 "+ (1.1805916 * Math.Pow(10,21)) % 71 );
                
                           // Console.ReadKey();                        
                        break;
                    default:
                       
                        Console.WriteLine();
                        g = Convert.ToInt32(Console.ReadLine());
                        if (Math.Pow(g, entering - 1) % entering != 1)
                        {
                            Console.WriteLine("Значение не удволетворяет условие");
                            goto Start;
                        }
                        G = g;
                        break;
                }
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.WriteLine(G);
                Console.ResetColor();
                Console.ReadLine();
                RandomCommonNumberG = G;
                
                SideASecretKeyGenerate();

                
            }
            private void SideASecretKeyGenerate()
            {
                Console.WriteLine("Сторона A введите секретный ключ");
                int secretKey = Convert.ToInt32(Console.ReadLine());
                SecretKeyFromA = secretKey;
                double openKey = Math.Pow(RandomCommonNumberG, secretKey) % RandomCommonNumberP;
                OpenKeyFromA = openKey;
                Console.WriteLine();
                SideBSecretKeyGenerate();
            }
            private void SideBSecretKeyGenerate()
            {
                Console.WriteLine("Сторона B введите секретный ключ");
                int secretKey = Convert.ToInt32(Console.ReadLine());
                SecretKeyFromB = secretKey;
                double openKey = Math.Pow(RandomCommonNumberG, secretKey)%RandomCommonNumberP;
                OpenKeyFromB = openKey;
                Console.WriteLine();
                execution();
            }
            private void execution()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(OpenKeyFromA+" "+OpenKeyFromB);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(SecretKeyFromA+" "+SecretKeyFromB);
                Console.ResetColor();
                //Console.WriteLine("K=x%p");
                Console.WriteLine("ПРОВЕРКА "+"A-"+OpenKeyFromA+" b-"+SecretKeyFromB+" P-"+RandomCommonNumberP+" K="+Math.Pow(OpenKeyFromA,SecretKeyFromB)%RandomCommonNumberP);
                //СТРОЧКОЙ ВЫШЕ КЛЮЧ ПОЛУЧЕННЫЙ НА СТОРОНЕ B И ОН НЕ ВЕРНЫЙ ,В ОСТАЛЬНОМ ВСЁ ПРАВИЛЬНО,ХОТЯ НЕ ФАКТ ПРОВЕРИТЬ ОТКРЫТЫЙ КЛЮЧ ОТ А
                Console.WriteLine("ПРОВЕРКА " + "A-" + OpenKeyFromB + " b-" + SecretKeyFromA + " P-" + RandomCommonNumberP + " K=" + Math.Pow(OpenKeyFromB, SecretKeyFromA) % RandomCommonNumberP);
                //Console.WriteLine(Math.Pow(OpenKeyFromA, SecretKeyFromB)%RandomCommonNumberP+" "+ Math.Pow(OpenKeyFromB, SecretKeyFromA)%RandomCommonNumberP);
                Console.ReadLine();
                
                Console.WriteLine("Мы получили открытые ключи от стороны A "+OpenKeyFromA+" и стороны B "+ OpenKeyFromB+"\n Собеседники обменялись этими ключами");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Сторона A вы приняли открытый ключ от стороны B и ,преобразовав его по формуле K=B ^ a % p" +
                    ",где a ваш секретный ключ,B открытый ключ от стороны B , получили \n "+Math.Pow(OpenKeyFromB,SecretKeyFromA) % RandomCommonNumberP +"-Общий ключ шифрования");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                
                Console.WriteLine("Сторона B вы приняли открытый ключ от стороны A и ,преобразовав его по формуле K= A ^ b % p " +
                    ",где b ваш секретный ключ ,A  открытый ключ от стороны A , получили \n "+Math.Pow(OpenKeyFromA,SecretKeyFromB) % RandomCommonNumberP+"-Общий ключ шифрования");
                Console.ReadKey();
                //проверить формулу получения открытого ключа ,проверить и желательно переделать формулу получения g, по всей видимости программа выдаёт неверное значение 
                //Открытые ключи верны,алгоритм подбора g-колхозный
            }

        }

                

    }
}
