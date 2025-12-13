[20251213연습.md](https://github.com/user-attachments/files/24141674/20251213.md)
# if
## 숫자 하나 입력받기
10 이상이면 "크다" 아니면 "작다"
```csharp
Console.WriteLine($"숫자를 입력해 주세요.");
   string input = Console.ReadLine();
   int.TryParse(input, out int number);
   if (number >= 10)
   {
       Console.WriteLine("크다.");
   }
   else
   {
       Console.WriteLine("작다.");
   }
```
## 사용자에게 정수 하나를 입력받아 아래 규칙에 따라 출력하세요.

입력한 숫자가 양수이면 → 양수입니다

입력한 숫자가 0이면 → 0입니다

입력한 숫자가 음수이면 → 음수입니다
```csharp
 Console.WriteLine("숫자를 입력하세요.");

 int number = int.Parse(Console.ReadLine());

 if (number < 0)
 {
     Console.WriteLine("음수 입니다.");
 }
 else if (number == 0)
 {
     Console.WriteLine("0 입니다.");
 }
 else
 {
     Console.WriteLine("양수 입니다.");
 }
```
## 사용자에게 정수 하나를 입력받아 다음을 출력하세요.

숫자가 짝수이면서 양수 → 양수 짝수

숫자가 짝수이면서 음수 → 음수 짝수

숫자가 홀수이면서 양수 → 양수 홀수

숫자가 홀수이면서 음수 → 음수 홀수

숫자가 0 → 0
```csharp
Console.WriteLine("숫자를 입력하세요.");

int number = int.Parse(Console.ReadLine());

if ((number > 0) && (number % 2 == 0))
{
    Console.WriteLine("짝수 이면서 양수 입니다.");
}
else if ((number < 0) && (number  % 2 == 0))
{
    Console.WriteLine("음수 이면서 짝수 입니다.");
}
else if (number > 0)
{
    Console.WriteLine("양수 이면서 홀수 입니다.");
}
else if (number < 0)
{
    Console.WriteLine("음수 이면서 홀수 입니다.");
}

else
{
    Console.WriteLine("0 입니다.");
}
```
# for
## 0부터 4까지 출력
```csharp
for (int i = 0; i <= 4; i++)
   {
       Console.WriteLine($"{i}");
   }
```

## 1부터 10까지 숫자를 출력하되짝수만 출력각 숫자 옆에 "짝수"라고 표시

```csharp
for (int i = 1; i < 11; i++)
{ 
    if (i % 2 == 0)
    {
        Console.WriteLine($"{i} 짝수");
    }
}
```
1부터 10까지 숫자 중
짝수만 더한 합을 출력하세요.
```csharp
int sum = 0;

for (int i = 1; i < 11; i++)
{

    if (i % 2 == 0)
    {
        sum += i;
    }
}
    Console.WriteLine("{0}", sum);
```
# 배열
## 배열 + 반복문
```csharp
 int[] number = new int[5];


 for (int i = 0; i < number.Length; i++)
 {
     number[i] = i + 1;

     Console.WriteLine(number[i]);
 }
```
