using System;
class Program
{

    static void Main()
    {
        string lang = "en"; //default language is English
        string input = "0"; //input string - numeric input for simplicity, especially w/ multilingual support

        double totalPoints = 0;
        double totalDeltaPoints = 0;
        int totalSamples = 0;

        while (input != "-1")
        {
            DisplayMenu(lang);
            input = Console.ReadLine();
            if (input == "4") break;
            switch (input)
            {
                case "1":
                    UnitConversion(lang);
                    break;
                case "2":
                    RockClassification(ref totalPoints, ref totalDeltaPoints, ref totalSamples, lang);
                    break;
                case "3":
                    LanguageOptions(ref lang);
                    break;
                default:
                    Console.WriteLine(InvalidInput(lang));
                    break;
            }
        }
    }









    //GENERAL UNIT CONVERSION SUBPROCESSES

    static double SafeDoubleParse(string input) {
        try {
            double output = double.Parse(input);
            return output;
        }
        catch (Exception e) {
            return -460;
        }
    }

    static int SafeIntParse(string input) {
        try {
            int output = int.Parse(input);
            return output;
        }
        catch (Exception e) {
            return -460;
        }
    }

    static void UnitConversion(string lang) {
        DisplayUnitTypePrompt(lang);
        string input = Console.ReadLine();
        if (input == "-1") return;
        else if (input == "1") LengthConversion(lang);
        else if (input == "2") MassConversion(lang);
        else if (input == "3") TempConversion(lang);
        else Console.WriteLine(InvalidInput(lang));
    }



    //LENGTH CONVERSION SUBPROCESSES
    static void LengthConversion(string lang) {
        string length1;
        string length2;
        while (true) { //loop gets input, output type//
            DisplayFirstLengthTypePrompt(lang); //get first mass
            length1 = Console.ReadLine();
            if (length1 == "-1") return;
            else length1 = RouteLengthMenu(length1, lang);
            if (length1 == "error") {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }

            DisplaySecondLengthTypePrompt(lang); //get second mass
            length2 = Console.ReadLine();
            if (length2 == "-1") return;
            else length2 = RouteLengthMenu(length2, lang);
            if (length2 == "error") {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }
            else break;
        }

        string inputLengthString;
        double inputLength;

        while (true) { //once input and output types are known, gets double for # to convert//
            Console.WriteLine(ConversionConfirmation(length1, length2, lang));
            Console.WriteLine(ConversionAmountPrompt (length1, lang));
            inputLengthString = Console.ReadLine();
            inputLength = SafeDoubleParse(inputLengthString);
            if (inputLength > 0) break;
            else {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }
        }
        double output = LengthConversionCalculation(length1, length2, inputLength);
        if (output < 0) Console.WriteLine(GeneralError(lang));
        else Console.WriteLine(SuccessfulConversionOutput(inputLength, output, length1, length2, lang));

    }

    static double LengthConversionCalculation(string type1, string type2, double input) {
        if (type1 == "mm" || type1 == "cm" || type1 == "m" || type1 == "km") {
            input = toMeters(type1, input); //converts to meters first for (slightly improved) simplcity
            if (type2 == "mm") return (input * 1000);
            else if (type2 == "cm") return (input * 100);
            else if (type2 == "m") return (input * 1);
            else if (type2 == "km") return (input * .001);
            else if (type2 == "in") return (input * 39.3701);
            else if (type2 == "yd") return (input * 1.0936);
            else if (type2 == "miles") return (input * .000621371); //extra precision
            else return -1;
        }
        else if (type1 == "in" || type1 == "yd" || type1 == "miles") {
                input = toYards(type1, input); //converts to meters first for (slightly improved) simplcity
                if (type2 == "mm") return (input * 914.4);
            else if (type2 == "cm") return (input * 91.4400);
                else if (type2 == "m") return (input * .9144);
                else if (type2 == "km") return (input * .0009144); //extra precision
                else if (type2 == "in") return (input * 36);
                else if (type2 == "yd") return (input * 1);
                else if (type2 == "miles") return (input / 1760.0000); //extra precision
                else return -1;
            }
        else return -1;
        }


    static double toMeters(string unit, double input) {
        if (unit == "mm") return (input / 1000.0); //to be honest it's inconsistent because i'm tired and it works regardless
        else if (unit == "cm") return (input / 100.0);
        else if (unit == "m") return (input);
        else if (unit == "km") return (input * 1000.0);
        else return 1;
    }

    static double toYards(string unit, double input) {
        if (unit == "in") return (input / 36.0000);
        else if (unit == "yd") return (input);
        else if (unit == "miles") return (input * 1760.0000);
        else return -1;
    }

    static string RouteLengthMenu(string input, string lang)
    {
        switch (input)
        {
            case "1":
                return "mm";
            case "2":
                return "cm";
            case "3":
                return "m";
            case "4":
                return "km";
            case "5":
                return "in";
            case "6":
                return "yd";
            case "7":
                return "miles";
            default:
                return "error";
        }
    }

    //MASS CONVERSION SUBPROCESSES
    static void MassConversion(string lang) {
        string mass1;
        string mass2;
        while (true) { //loop gets input, output type//
            DisplayFirstMassTypePrompt(lang); //get first mass
            mass1 = Console.ReadLine();
            if (mass1 == "-1") return;
            else mass1 = RouteMassMenu(mass1, lang);
            if (mass1 == "error") {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }

            DisplaySecondMassTypePrompt(lang); //get second mass
            mass2 = Console.ReadLine();
            if (mass2 == "-1") return;
            else mass2 = RouteMassMenu(mass2, lang);
            if (mass2 == "error") {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }
            else break;
        }

        string inputMassString;
        double inputMass;

        while (true) { //once input and output types are known, gets double for # to convert//
            Console.WriteLine(ConversionConfirmation(mass1, mass2, lang));
            Console.WriteLine(ConversionAmountPrompt(mass1, lang));
            inputMassString = Console.ReadLine();
            inputMass = SafeDoubleParse(inputMassString);
            if (inputMass > 0) break;
            else {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }
        }

        double output = MassConversionCalculation(mass1, mass2, inputMass);
        if (output < 0) Console.WriteLine(GeneralError(lang));
        else Console.WriteLine(SuccessfulConversionOutput(inputMass, output, mass1, mass2, lang));
    }


    static string RouteMassMenu(string input, string lang)
    {
        switch (input)
        {
            case "1":
                return "oz";
            case "2":
                return "lb";
            case "3":
                return "g";
            case "4":
                return "kg";
            default:
                return "error";
        }
    }

    static double MassConversionCalculation(string type1, string type2, double input) {
        if (type1 == "oz") { //numbers are conversion factors
            if (type2 == "oz") return (input * 1.0000); 
            else if (type2 == "lb") return (input * .0625);
            else if (type2 == "g") return (input * 28.3945);
            else if (type2 == "kg") return (input * .0283);
            else return -1;
        }
        else if (type1 == "lb") {
            if (type2 == "oz") return (input * 16.0000); 
            else if (type2 == "lb") return (input * 1.0000);
            else if (type2 == "g") return (input * 453.5920);
            else if (type2 == "kg") return (input * .4536);
            else return -1;
        }
        else if (type1 == "g") {
            if (type2 == "oz") return (input * .0353); 
            else if (type2 == "lb") return (input * .0022);
            else if (type2 == "g") return (input * 1.0000);
            else if (type2 == "kg") return (input * .0010);
            else return -1;
        }
        else if (type1 == "kg") {
            if (type2 == "oz") return (input * 35.274); 
            else if (type2 == "lb") return (input * 2.2050);
            else if (type2 == "g") return (input * 1000);
            else if (type2 == "kg") return (input * 1);
            else return -1;
        }
        else return -1;
    }

    //TEMP CONVERSION SUBPROCESSES
    static void TempConversion(string lang) {
        string type;
        while (true) { //only two possible types, so simpler than other menus
            DisplayTempMenu(lang); //get first length
            type = Console.ReadLine();
            if (type == "-1") return;
            else if (type == "1" || type == "2") break;
            else {
                Console.WriteLine(InvalidInput(lang));
                continue;
                }
        }

        string inputTempString;
        double inputTemp;
        while (true) { //once input and output types are known, gets double for # to convert//
            if (type == "1") {
                Console.WriteLine(ConversionConfirmation("F°", "C°", lang));
                Console.WriteLine(ConversionAmountPrompt("F°", lang));
                }
            if (type == "2") {
            Console.WriteLine(ConversionConfirmation("C°", "F°", lang));
            Console.WriteLine(ConversionAmountPrompt("C°", lang));
                }
            inputTempString = Console.ReadLine();
            inputTemp = SafeDoubleParse(inputTempString);

            if (inputTemp > -460) break; //-460 - closest integer below absolute zero Fahrenheit. In hindsight should have made const//
            else {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }
        }
        double output = TempConversionCalculation(type, inputTemp);
        if (output <= -460) Console.WriteLine(InvalidInput(lang));
        else {
            if (type == "1") Console.WriteLine(SuccessfulConversionOutput(inputTemp, output, "F°", "C°", lang));
            else if (type == "2") Console.WriteLine(SuccessfulConversionOutput(inputTemp, output, "C°", "F°", lang));
            else Console.WriteLine(GeneralError(lang));
        } 
    }

    static double TempConversionCalculation(string type, double inputTemp) {
        if (type == "1") return (inputTemp - 32) * 5 / 9; //F to C
        else if (type == "2") return (inputTemp * 9 / 5) + 32; //C to F
        else return -460;
    }



    //ROCK CLASSIFICATION SUBPROCESSES

    static void RockClassification(ref double totalPoints, ref double totalDeltaPoints, ref int totalSamples, string lang) {
        string input;
        while (true) {
            DisplayRockMenu(lang);
            input = Console.ReadLine();
            if (input == "-1") break;
            else if (input == "1") AddNewRock(ref totalPoints, ref totalDeltaPoints, ref totalSamples, lang);
            else if (input == "2") DisplayRockStats(totalPoints, totalDeltaPoints, totalSamples, lang);
            else {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }
        }
    }

    static void AddNewRock(ref double totalPoints, ref double totalDeltaPoints, ref int totalSamples, string lang) {
        double currentPoints = 0;
        string currentInput;
        while (true) { //each while loop is a new criteria and point calculation
            Console.WriteLine(RockPrompts("num_rocks", lang));
            currentInput = Console.ReadLine();
            int numSamples = SafeIntParse(currentInput);
            if (numSamples > 0) {
                currentPoints += (numSamples * 4.5);
                break;
            }
            else {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }
        }

        while (true) {
            Console.WriteLine(RockPrompts("transport", lang));
            currentInput = Console.ReadLine();
            if (currentInput == "1") { currentPoints += 7.3; break; } 
            else if (currentInput == "0") break;
            else {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }
        }

        while (true) {
            Console.WriteLine(RockPrompts("temperature", lang));
            currentInput = Console.ReadLine();
            double temp = SafeDoubleParse(currentInput);
            if (temp <= 0 && temp > -460) { currentPoints += 9.2; break; }
            else if (temp >= 0) break;
            else {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }
        }

        while (true) {
            Console.WriteLine(RockPrompts("mass", lang));
            currentInput = Console.ReadLine();
            double mass = SafeDoubleParse(currentInput);
            if (mass >= 25) { currentPoints *= 1.17; break;}
            else if (mass >= 0) break;
            else {
                Console.WriteLine(InvalidInput(lang));
                continue;
            }
        }

        totalPoints += currentPoints;
        totalSamples += 1;
        deltaMax = currentPoints;

        while (true) {
            Console.WriteLine(RockPrompts("new_value", lang) + " " + currentPoints);
            Console.WriteLine(RockPrompts("buff", lang));
            currentInput = Console.ReadLine();
            if (currentInput == "0") break;
            else if (currentInput == "1") {
                Console.WriteLine(RockPrompts("buff_value", lang));
                double currentDelta;
                currentInput = Console.ReadLine();
                currentDelta = SafeDoubleParse(currentInput);
                if (currentDelta > deltaMax || currentDelta < (currentPoints * -1) || currentDelta == -460) { 
                //acknowledging i failed to fix magic number -460 issue if this is still here
                    Console.WriteLine(RockPrompts("buff_value_amount_error", lang));
                    continue;
                }
            else { currentPoints += currentDelta; totalDeltaPoints += currentDelta; continue; }
            }
            else {
                Console.WriteLine(InvalidInput(lang));
                continue;

            }
        }
    }





    //LANGUAGE SETTINGS


    static void ChangeLanguage(ref string lang, string newLang)
    {
        lang = newLang; //change language
        Console.WriteLine(LanguageChangeConfirmation(lang));
    }

    static void LanguageOptions(ref string lang)
    {
        while (true)
        {
            DisplayLanguageMenu(lang);
            string input;
            input = Console.ReadLine();
            if (input == "-1") return;
            switch (input)
            {
                case "1":
                    ChangeLanguage(ref lang, "en");
                    return;
                case "2":
                    ChangeLanguage(ref lang, "fr");
                    return; //ensure this is updated for added language support
                default:
                    Console.WriteLine(InvalidInput(lang));
                    break;
            }
        }
    }

    //MULTILINGUAL MENU DISPLAYS

    static void DisplayMenu(string languageCode)
    {
        if (languageCode == "en")
        {
            Console.WriteLine("\nPlease enter the number of an option from the menu below.\n");
            Console.WriteLine("1. Convert units of measure");
            Console.WriteLine("2. Rock classification");
            Console.WriteLine("3. Change language");
            Console.WriteLine("4. Exit");
        }
        if (languageCode == "fr")
        {
            Console.WriteLine("\nVeuillez entrer le numéro d'une option parmi le menu ci-dessous.\n");
            Console.WriteLine("1. Convertir des unités de mesure");
            Console.WriteLine("2. Classification des roches");
            Console.WriteLine("3. Changer de langue");
            Console.WriteLine("4. Quitter");
        }
    }

    static void DisplayLanguageMenu(string languageCode)
    {

        Console.WriteLine(SubmenuHeader(languageCode));
        //display list of options
        Console.WriteLine("1. English (en)");
        Console.WriteLine("2. Français (fr)");
        //further language options can be added here

    }

    static void DisplayUnitTypePrompt(string languageCode)
    {
        if (languageCode == "en") Console.WriteLine("\nWhich type of unit would you like to convert?");
        if (languageCode == "fr") Console.WriteLine("\nQuel type d'unité souhaitez-vous convertir?");
        Console.WriteLine(SubmenuHeader(languageCode));
        if (languageCode == "en")
        {
            Console.WriteLine("1. Length");
            Console.WriteLine("2. Mass");
            Console.WriteLine("3. Temperature");
        }
        if (languageCode == "fr")
        {
            Console.WriteLine("1. Longueur");
            Console.WriteLine("2. Masse");
            Console.WriteLine("3. Température");
        }
    }

    static void DisplayMassList(string languageCode)
    { //language code kept for consistency
        Console.WriteLine("1. oz");
        Console.WriteLine("2. lb");
        Console.WriteLine("3. g");
        Console.WriteLine("4. kg");
    }

    static void DisplayLengthList(string languageCode)
    { //language code kept for consistency
        Console.WriteLine("1. mm");
        Console.WriteLine("2. cm");
        Console.WriteLine("3. m");
        Console.WriteLine("4. km");
        Console.WriteLine("5. in");
        Console.WriteLine("6. yd");
        Console.WriteLine("7. miles");

    }

    static void DisplayFirstMassTypePrompt(string languageCode)
    {
        if (languageCode == "en") Console.WriteLine("\nWhich unit are you converting from?");
        if (languageCode == "fr") Console.WriteLine("\nDe quel unité convertissez-vous?");
        Console.WriteLine(SubmenuHeader(languageCode));
        DisplayMassList(languageCode);
    }

    static void DisplaySecondMassTypePrompt(string languageCode)
    {
        if (languageCode == "en") Console.WriteLine("\nWhich unit are you converting to?");
        if (languageCode == "fr") Console.WriteLine("\nVers quel unité convertissez-vous?");
        Console.WriteLine(SubmenuHeader(languageCode));
        DisplayMassList(languageCode);
    }

    static void DisplayFirstLengthTypePrompt(string languageCode)
    {
        if (languageCode == "en") Console.WriteLine("\nWhich unit are you converting from?");
        if (languageCode == "fr") Console.WriteLine("\nDe quel unité convertissez-vous?");
        Console.WriteLine(SubmenuHeader(languageCode));
        DisplayLengthList(languageCode);
    }

    static void DisplaySecondLengthTypePrompt(string languageCode)
    {
        if (languageCode == "en") Console.WriteLine("\nWhich unit are you converting to?");
        if (languageCode == "fr") Console.WriteLine("\nVers quel unité convertissez-vous?");
        Console.WriteLine(SubmenuHeader(languageCode));
        DisplayLengthList(languageCode);
    }

    static void DisplayTempMenu(string languageCode) {
        if (languageCode == "en") Console.WriteLine("\nWhich conversion are you performing?");
        if (languageCode == "fr") Console.WriteLine("\nQuelle conversion effectuez-vous?");
        Console.WriteLine(SubmenuHeader(languageCode));
        Console.WriteLine("1. F to C");
        Console.WriteLine("2. C to F");
    }

    static void DisplayRockMenu(string languageCode) {
        Console.WriteLine(SubmenuHeader(languageCode));
        if (languageCode == "en") {
            Console.WriteLine("1. Add new sample");
            Console.WriteLine("2. View totals");
        }
        if (languageCode == "fr") {
            Console.WriteLine("1. Ajouter un nouvel échantillon");
            Console.WriteLine("2. Voir les totaux");
        }
    }

    static void DisplayRockStats(double totalPoints, double totalDeltaPoints, int totalSamples, string languageCode) {
        if (languageCode == "en") {
            Console.WriteLine("\nTotal points: " + totalPoints);
            Console.WriteLine("Total points added/subtracted: " + totalDeltaPoints);
            Console.WriteLine("Number of rock samples: " + totalSamples);
        }
        else if (languageCode == "fr") {
            Console.WriteLine("\nTotal des points: " + totalPoints);
            Console.WriteLine("Total des points ajoutés/soustraits: " + totalDeltaPoints);
            Console.WriteLine("Nombre d'échantillons de roches: " + totalSamples);
        }
    }



    //MULTILINGUAL STRINGS

    static string LanguageChangeConfirmation(string languageCode)
    {
        if (languageCode == "en") return "\nLanguage is now English.\n"; //confirmation messages
        if (languageCode == "fr") return "\nLa langue est maintenant le français.\n";
        return "Language error";
    }


    static string InvalidInput(string languageCode)
    {
        if (languageCode == "en") return "Invalid input. Please try again.";
        if (languageCode == "fr") return "Entrée invalide. Veuillez réessayer.";
        return "Language error";
    }

    static string GeneralError(string languageCode) {
        if (languageCode == "en") return "Unknown error. Please try again.";
        if (languageCode == "fr") return "Erreur inconnue. Veuillez réessayer.";
        return "Language error";
    }

    static string SubmenuHeader(string languageCode)
    {
        if (languageCode == "en") return "\nPlease enter the number of an option from the menu below or '-1' to exit\n";
        if (languageCode == "fr") return "\nVeuillez entrer le numéro d'une option parmi le menu ci-dessous ou '-1' pour quitter\n";
        return "Language error";
    }

    static string ConversionConfirmation(string massType1, string massType2, string lang) {
        if (lang == "en") return "\nConverting from " + massType1 + " to " + massType2 + ".";
        if (lang == "fr") return "\nConversion de " + massType1 + " vers " + massType2 + ".";
        return "Language error";
    }

    static string ConversionAmountPrompt(string inputType, string lang) {
        if (lang == "en") return "\nHow many " + inputType + "?";
        if (lang == "fr") return "\nCombien de " + inputType + "?";
        return "Language error";
    }

    static string SuccessfulConversionOutput(double input, double output, string massType1, string massType2, string lang) {
        input = Math.Round(input, 4);
        output = Math.Round(output, 4);
        return ("\n" + input + " " + massType1 + " = " + output + " " + massType2 + ".");
    }


    static string RockPrompts(string promptName, string lang) {
        if (lang == "en") {
            if (promptName == "num_rocks") return "\nHow many identical rocks make up the sample?";
            if (promptName == "transport") return "\nWill the sample need transportation? (1 for yes, 0 for no)";
            if (promptName == "temperature") return "\nWhat is the surface temperature of the sample? (in Celcius)";
            if (promptName == "mass") return "\nWhat is the mass of the sample? (in kg)";
            if (promptName == "new_value") return "\nNew sample point value:";
            if (promptName == "buff") return "\nWould you like to alter the point value? (1 for yes, 0 for no)";
            if (promptName == "buff_value") return "\nBy what amount would you like to change the point value?";
            if (promptName == "buff_value_amount_error") return "\nThe sample's point value cannot be altered by more than the original point value.";
            return "Error";
        }
        else if (lang == "fr") {
            if (promptName == "num_rocks") return "\nCombien de roches identiques composent l'échantillon?";
            if (promptName == "transport") return "\nEst-ce que l'échantillon aura besoin d'un transport? (1 pour oui, 0 pour non)";
            if (promptName == "temperature") return "\nQuelle est la température de surface de l'échantillon? (en Celcius)";
            if (promptName == "mass") return "\nQuelle est la masse de l'échantillon? (en kg)";
            if (promptName == "new_value") return "\nNouvelle valeur du point d'échantillon:";
            if (promptName == "buff") return "\nSouhaitez-vous modifier la valeur du point? (1 pour oui, 0 pour non)";
            if (promptName == "buff_value") return "\nDe combien souhaitez-vous modifier la valeur du point?";
            if (promptName == "buff_value_amount_error") return "\nLa valeur du point de l'échantillon ne peut pas être modifiée de plus que la valeur du point d'origine.";
            return("Erreur");
        }
        else return "Language error";
    }



}

