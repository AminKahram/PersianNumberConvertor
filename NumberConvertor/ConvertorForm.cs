using static System.Net.Mime.MediaTypeNames;

namespace NumberConvertor;

public partial class ConvertorForm : Form
{
    public Dictionary<byte, string> numberDic { get; set; }
    public Dictionary<byte, string> exceptionDic { get; set; }
    public Dictionary<byte, string> unitDic { get; set; }
    public Dictionary<byte, string> floatUnitDic { get; set; }
    public ConvertorForm()
    {
        this.KeyPreview = true;
        this.KeyPress += Converter_Click;
        InitializeComponent();

        numberDic = new Dictionary<byte, string>
        {
            {1," یک "},
            {2," دو "},
            {3," سه "},
            {4," چهار "},
            {5," پنج "},
            {6," شش "},
            {7," هفت "},
            {8," هشت "},
            {9," نه "},
            {10," ده "},
            {11," یازده "},
            {12," دوازده "},
            {13," سیزده "},
            {14," چهارده "},
            {15," پانزده "},
            {16," شانزده "},
            {17," هفده "},
            {18," هجده "},
            {19," نوزده "},
            {20," بیست "},
            {30," سی "},
            {40," چهل "},
            {50," پنجاه "},
            {60," شصد "},
            {70," هفتاد "},
            {80," هشتاد "},
            {90," نود "},
        };
        exceptionDic = new Dictionary<byte, string>
        {
            {2," دویست "},
            {3," سیصد "},
            {5," پانصد "},
        };
        unitDic = new Dictionary<byte, string>
        {
            {0, "" },
            {1," هزار "},
            {2," میلیون "},
            {3," میلیارد "},
            {4," همت "},
            {5," بیلیارد "},
            {6," تریلیون "},
            {7," Udefined "},
        };
        floatUnitDic = new Dictionary<byte, string>
        {
            {1," دهم " },
            {2," صدم "},
            {3," هزارم "},
            {4," میلیونم "},
            {5," میلیاردم "},
            {6," Out of range "},
        };
    }
    private List<string> SplitIntoThreeDigitParts(string number)
    {
        var parts = new List<string>();
        // Iterate through the string from right to left
        for (int i = number.Length; i > 0; i -= 3)
        {
            // Extract a three-digit part, convert it to an integer, and add it to the list
            string part = number.Substring(Math.Max(0, i - 3), Math.Min(3, i - Math.Max(0, i - 3)));
            parts.Insert(0, part);
        }

        return parts;
    }
    private string GetThreeDigitValue(string number)
    {
        string result = "";

        byte left = byte.Parse(number.Substring(0, 1));
        string right = number.Substring(1, 2);

        if (left != 2 && left != 3 && left != 5)
        {
            result = string.Concat(result, numberDic[left]);
            result = string.Concat(result, " صد ");
        }
        else
            result = string.Concat(result, exceptionDic[(left)]);


        if (byte.Parse(right) > 0)
        {
            result = string.Concat(result, "و");
            result = string.Concat(result, GetTwoDigitValue(right));
        }

        return result;
    }
    private string GetTwoDigitValue(string number)
    {
        string result = "";

        byte left = byte.Parse(number.Substring(0, 1));
        byte right = byte.Parse(number.Substring(1, 1));

        if (left == 1)
            result = string.Concat(result, numberDic[(byte)(left * 10 + right)]);
        else
        {
            result = string.Concat(result, numberDic[(byte)(left * 10)]);
            result = string.Concat(result, "و");
            result = string.Concat(result, GetOneDigitValue(right.ToString()));
        }

        return result;
    }
    private string GetOneDigitValue(string number)
        => numberDic[byte.Parse(number)];
    

    private void ConvertorForm_Load(object sender, EventArgs e)
    {
        this.KeyUp += new System.Windows.Forms.KeyEventHandler(Converter_Click);
    }
    private string IntegerNumberConvertor(string number)
    {
        string result = "";

        // returning zero value  
        if (float.Parse(number) == 0)
            return "صغر";

        // splitig our number into string and collectin it in a list
        List<string> numberSections = SplitIntoThreeDigitParts(number);

        // getting how many sections do we have for unit measurment
        byte sections = (byte)numberSections.Count();

        // looping on our number list for number evaluation 
        // in here we selected value and i as index for each number 
        foreach (var numberSection in numberSections.Select((value, i) => new { i, value }))
        {
            byte numberLen = (byte)numberSection.value.Length;

            // base on number we send our data to its related function
            if (numberLen > 2)
            {
                result = string.Concat(result, GetThreeDigitValue(numberSection.value));
            }
            else if (numberLen > 1)
            {
                result = string.Concat(result, GetTwoDigitValue(numberSection.value));
            }
            else
            {
                result = string.Concat(result, GetOneDigitValue(numberSection.value));
            }


            // concating each values unit and checking units to not hit more than our dictionary 
            result = string.Concat(result, unitDic[(byte)((sections > 6 ? 8 : sections) - 1)]);

            // adding و  at the end  of each unit
            if (numberSection.i == 0)
                result = string.Concat(result, "و");

            // decreasing section to get suitable unit value
            if (sections != 1)
                sections--;

        }

        return result;
    }

    private string IntegerNumberConvertor(long number)
    {
        string result = "";

        if (number == 0)
            return "صغر";
        // save og number
        long ogNumber = number;

        // how many digits do we have on ten base
        byte tenBase = (byte)Math.Floor(Math.Log10(number));

        // how many section we have
        byte section = (byte)(Math.Floor((double)(tenBase / 3)) + 1);

        // if the number we have was in hundred range
        // this also is where our recursive function retrieves data
        if (tenBase < 3)
        {
            // itterating on our number until we hit zero
            do
            {
                // getting each number value by dividng it to ten base 
                number = ogNumber / (long)Math.Pow(10, tenBase);

                // removing the rest of the number out of equation
                ogNumber = ogNumber - number * (long)Math.Pow(10, tenBase);

                // bypassing when the number hits zero
                if (number != 0)
                {

                    // getting string value of hundred
                    if (tenBase == 2)
                    {
                        // 2, 3 and 5 hundred values has exceptional values out of others
                        if (number != 2 && number != 3 && number != 5)
                        {
                            result = string.Concat(result, numberDic[(byte)number]);
                            result = string.Concat(result, unitDic[1]);
                        }
                        else
                            result = string.Concat(result, exceptionDic[(byte)(number)]);
                    }

                    // getting string value of tens
                    else if (tenBase == 1)
                    {
                        if (number > 1)
                            result = string.Concat(result, numberDic[(byte)(number * 10)]);

                        else
                            return string.Concat(result, numberDic[(byte)(number * 10 + ogNumber)]);
                    }

                    // getting string value of single digits
                    else
                        result = string.Concat(result, numberDic[(byte)number]);

                    // adding & at the end of each number when our number doesn't hit the zero value
                    if (ogNumber > 0)
                        result = string.Concat(result, "و");
                }
                // decreasing the value of tenbase to calculate numbers lessr than our corrent number base
                tenBase--;

            } while (ogNumber > 0);

            return result;
        }

        // itterating on our number when its has more than three digits
        do
        {
            // getting first 3 digits on the left out of our number by dividng it to tenth power of amout of digits lefted from 3 digit sections we have  
            number = ogNumber / (long)Math.Pow(10, (section - 1) * 3);

            // removing the rest of the number out of equation
            ogNumber = ogNumber - number * (long)Math.Pow(10, (section - 1) * 3);

            // bypassing when the number hits zero
            if (number != 0)
            {
                // sending the number on hand to the function and concating the value to rest of the result recursively
                result = string.Concat(result, IntegerNumberConvertor(number));

                // checking section value to add on unit value
                if (section > 1 && section < 8)
                    result = string.Concat(result, unitDic[(byte)(section)]);

                // decreasing the value of section to calculate numbers lesser than our corrent number base
                section--;

                // adding & at the end of each number when our number doesn't hit the zero value
                if (ogNumber > 0)
                    result = string.Concat(result, "و");
            }

        } while (ogNumber > 0);

        return result;
    }

    private string floatNumberConvertor(string number)
    {
        string result = "";
        // spliting the string value 
        var stringValue = number.Split('.');


        if (stringValue[0] != "0")
        {
            result = string.Concat(result, IntegerNumberConvertor(Convert.ToInt64(stringValue[0])));
            result = string.Concat(result, "و");
        }
        if (!string.IsNullOrEmpty(stringValue[1]) && stringValue[1] != "0")
        {
            // checking for int value on second part and returning error when it exceeds
            if (int.TryParse(stringValue[1], out int decimalLongValue))
            {
                // getting the number value of second part of our string 
                result = string.Concat(result, IntegerNumberConvertor(stringValue[1]));

                // this code is for long version of IntegerNumberConvertor methode
                //result = string.Concat(result, IntegerNumberConvertor(decimalLongValue));

                byte secondPartStringLen = (byte)(stringValue[1].Length);

                // checking len of second part for ده ‌& صد iteration on each secttion count
                if (secondPartStringLen > 3)
                {
                    byte decimalSection = (byte)(secondPartStringLen % 3);

                    if (decimalSection == 1)
                        result = string.Concat(result, "ده");

                    else if (decimalSection == 2)
                        result = string.Concat(result, "صد");

                    byte floatSection = (byte)(secondPartStringLen / 3 + 2);
                    
                    result = string.Concat(result, floatUnitDic[floatSection]);
                }
                else
                    result = string.Concat(result, floatUnitDic[secondPartStringLen]);

            }
            else
                result = floatUnitDic[6];

        }
        return result;
    }
    private void Number_KeyPress(object sender, KeyPressEventArgs e)
    {
        // ignoring all charecters but numbers and control
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-')
        {
            e.Handled = true;
        }
        // ignoring the second . in the string for float value
        if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
        {
            e.Handled = true;
        }
        // ignoring sencond - for negative values
        if (e.KeyChar == '-' && (sender as TextBox).Text.Contains("-"))
        {
            e.Handled = true;
        }
        // ignoring - after a digit entery
        if ((sender as TextBox).Text.Any(char.IsDigit) && e.KeyChar == '-')
        {
            e.Handled = true;
        }
    }


    private void Converter_Click(object sender, EventArgs e)
    {

        string result = "";

        string text = Number.Text;

        // adding minus for - value entery
        if (text.Contains('-') && !result.Contains("منفی"))
        {
            text = text.Replace('-', ' ');
            result = string.Concat(result, " منفی");
        }

        // checking for null and empty enteries
        if (!string.IsNullOrWhiteSpace(text))
        {
            //this code is for string version of the code 

            if (text.Contains('.') && float.Parse(text) != 0)
            {
                result = string.Concat(result, floatNumberConvertor(text));
            }
            else
            {
                result = string.Concat(result, IntegerNumberConvertor(text));
                result = string.Concat(result, " تومان ");
            }


            // this code is for long version of IntegerNumberConvertor methode

            //// checking for long value entery            
            //if (long.TryParse(text, out long longValue))
            //{
            //    result = string.Concat(result, IntegerNumberConvertor(longValue));

            //    //adding toman value at the end of the string
            //    result = string.Concat(result, " تومان ");
            //}
            //// checking for float value entery
            //else if (float.TryParse(text, out float floatValue) && text.Contains('.'))
            //{
            //    if (text != "0")
            //        result = string.Concat(result, floatNumberConvertor(text));
            //}
            //else
            //    result = unitDic[7];

        }
        Result.Text = result;

    }


}