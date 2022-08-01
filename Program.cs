Console.WriteLine("Please enter Card number:");
var userInput = Console.ReadLine();
long card = (long)Convert.ToDouble(userInput);
long[] cardNumber = GetNumberArr(card);
int digitCount = cardNumber.Length;
bool result = VerifyCardNumber(digitCount, cardNumber); //call to Luhn's algo
//if card verified, check the card type
if (result)
{
    Console.WriteLine(DefineCard(cardNumber, digitCount));
}
else
{
    Console.WriteLine("INVALID");
}

bool VerifyCardNumber(int digitCount, long[] cardDigits)
{
    long oddSum = 0;
    long evenProductSum = 0;
    for (int i = 0; i < digitCount; i++)
    {
        //even number have to be *2 and result products have to be summed up into evenProducSum ( example 6*2=12 -> sum( 1 + 2) ) 
        if((i+1) % 2 == 0) 
        {
            var sum = cardDigits[i] * 2;
            if (sum >= 10)
            {
                //since it can't be bigger than 18, we assume if it's >= 10, then "1" is the only possible value of first product
                evenProductSum += 1 + (sum % 10);
            }
            else
                evenProductSum += sum;
        }
        else //odd numbers going to be summed
        {
            oddSum += cardDigits[i];
        }
    }
    var totalSum = oddSum + evenProductSum;
    if (totalSum % 10 == 0)
        return true;
    return false;
}

long[] GetNumberArr(long card) //this returns the card number in reverse sequence because I need it so.
{
    List<long> result = new List<long>();
    while(card > 0)
    {
        result.Add(card % 10);
        card = card / 10;
    }
    return result.ToArray();
}

string DefineCard(long[] card, int digits)
{
    //first two digits are lastone*10 + prelast (the array is reverse of the userinput provided!)
    var firstDigits = card[digits - 1] * 10 + card[digits - 2];
    if (digits == 15 && (firstDigits == 37 || firstDigits == 34))
    {
        return "AMEX";
    }
    else if (digits == 16)
    {
        if (firstDigits >= 51 && firstDigits < 56)
            return "MASTERCARD";
        else if (firstDigits % 40 < 10)
            return "VISA";
    }
    else if (digits == 13 && firstDigits % 40 < 10)
        return "VISA";
    return "INVALID!!!";
}