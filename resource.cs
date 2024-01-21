// class invariants:
/*
 *    -Constructor:
 *     The constructor will be instantiated with 2 string arrays, one input and one output array.
 *     The string in the array is composed an integer, a space, and a resource.
 *     Both the input and output array are not empty. (Throw Exception)
 *     Precondition: N/A
 *     PostCondition: A cookie object was created.
 *    -IsActive:
 *     The IsActive method is used to track the state.
 *     The IsActive method will returns a bool.
 *     Precondition: N/A
 *     Postcondition: N/A
 *    -SetProficiencyLevel:
 *     This method defines the setting for the proficiency level, it also allows changing to
 *     the proficiency level. The proficiency level starts from 0 and stop increasing when it
 *     hits 2. (Throw Exception)
 *     Precondition: the profeciency level must not be outside of the range 0-2. State is active.
 *     Postcondition: the state of the profeciency level may be different if the starting proficiency
 *     level is different.
 *    -GetProficiencyLevel:
 *     This methods obtains the current proficiency level. This methods will returns an integer.
 *     Precondition: State is active.
 *     Postcondition: N/A (proficiency level did not change)
 *    -Apply:
 *     This methods allows the client to simulate the outcome of applying the formula.
 *     Precondition: State is active.
 *     Postcondition: N/A
 *       
*/

using System;
using System.Collections.Generic;

namespace PA1 {
   public class Formula {
      
      private const int MinProficiency = 0;
      private const int MaxProficiency = 2;
      private const int initialProficiency = 0;
      private const double partialModifier = 0.75;
      private const double extraModifier = 1.25;
      private int proficiencyLevel;
      private bool isActive;
      private int failureThreshold;
      private const double chanceOfSuccess = 0.75;
      private const double chanceOfSuccessIncreaseLeveledUp = 0.05;
      private string[]? inputNames;
      private string[]? outputNames;
      private int[]? inputQuantities;
      private int[]? outputQuantities;
      private int expectedQuantityThreshold;
      private int partialQuantityThreshold;

      private Random randomGenerator = new Random();

      // Constructor for the Formula class
      public Formula(string[] input, string[] output) {
         if (input == null || output == null || input.Length == 0 || output. Length == 0) {
            throw new ArgumentNullException();
         }

         // Define input name and quantities
         inputNames = new String[input.Length];
         inputQuantities = new int[input.Length];

         // Format the input
         for (int i = 0; i < input.Length; i++) {
            // input is an integer, a space, a resource
            string[] parts = input[].Split(' ');
            // Handle invalid input format
            if (parts.Length!= 2|| !int.TryParse(parts[0], out inputQuantities[i])) {
               throw new ArgumentException("Invalid input format.");
            }
            inputNames[i] = parts[1];
         }

         // Define output name and quantities
         outputNames = new string[output.Length];
         outputQuantities = new int[output.Length];

         // Format the output 
         for (int i = 0; i < output.Length; i++) {
            string[] parts = output[i].Split(' ');
            // Handle invalid output format
            if (parts.Length!= 2|| !int.TryParse(parts[0], out outputQuantities[i])) {
               throw new ArgumentException("Invalid output format.");
            }
            outputNames[i] = parts[1];
         }

         isActive = true;
         SetProficiencyLevel(initialProficiency);
      }

      // Precondition: state is active
      // Postcondition: N/A
      // This method will obtain the current proficiency level 
      public int GetProficiencyLevel() {
         if (!isActive) {
            throw new StateNotActiveException("The state is not active.");
         }
         int result = proficiencyLevel; 
      }

      // Precondition: the profeciency level must not be outside of the range 0-2. State is active.
      // Postcondition: the state of the profeciency level may be different if the starting proficiency level is different.
      // This method will set the proficiency level based on the preference of the client
      public void SetProficiencyLevel(int level) {
         // precondition: state is active
         if (!isActive) {
            throw new StateNotActiveException("The state is not active.");
         }
         // Make sure proficiencyLevel is within reasonable bounds
         if (level < MinProficiency || level > MaxProficiency){
            throw new ArgumentOutOfRangeException("level", "level is out of range");
         }
         proficiencyLevel = level;
         // increases the proficiency level decrease the failure threshold
         // increases the proficiency level decrease the partialQuantityThreshold
         // increases the proficiency level increase the expectedQuantityThreshold
         if (proficiencyLevel == 0) {
            failureThreshold = 35;
            partialQuantityThreshold = 25;
            expectedQuantityThreshold = 85;
         }
         if (proficiencyLevel == 1) {
            failureThreshold = 25;
            partialQuantityThreshold = 20;
            expectedQuantityThreshold = 90;
         } else {
            failureThreshold = 20;
            partialQuantityThreshold = 15;
            expectedQuantityThreshold = 95;         
         }
      }

      // precondition: state is active, inputName is not null
      // postcondition: input name will be stored in an array
      // This method allows the client to obtain the inputname
      public string[] getInputNames() {
         
         if (!isActive) {
            throw new StateNotActiveException("The state is not active.");
         }
         // Store the original array to avoid modified by external code
         string[] storedInputNames = new string[inputNames.Length];
         for (int i = 0; i < inputNames.Length; i++) {
            storedInputNames[i] = inputNames[i]; 
         }
         return storedInputNames;
      }

      // precondition: state is active, outputName is not null
      // postcondition: output name might be different from the inputName
      // This method allows the client to obtain the outputname
      public string[] getOutputNames() {
         // precondition: state is active
         if (!isActive) {
            throw new StateNotActiveException("The state is not active.");
         }
         strings[] storedOutputnames = new string[outputNames.Length];
         for (int i = 0; i < outputNames.Length; i++) {
            storedOutputNames[i] = outputNames[i];
         }
         return storedOutputNames;
      }

      // precondition: state is active
      // postcondition: inputQuantities may or may not be zero
      // This method allows the client to obtain the input quantities
      public int[] getInputQuantities() {
         if (!isActive) {
            throw new InvalidOperationException("The state is not active.");
         }
         int[] storedInputQuantities = new int[inputQuantities.Length];
         for (int i = 0; i < inputQuantities.Length; i++) {
            storedInputQuantities[i] = inputQuantities[i];
         }
         return storedInputQuantities;
      }

      // precondition: state is active
      // postcondition: outputQuantities may be different from inputQuantities
      // This method allows the client to obtain output quantities
      public int[] getOutputQuantites() {
         if (!isActive) {
            throw new InvalidOperationException("The state is not active.");
         }
         int[] storedOutputQuantities = new int[outputQuantities.Length];
         for (int i = 0; i < outputQuantities.Length; i++) {
            storedOutputQuantities = outputQuantities[i];
         }
         return storedOutputQuantities;
      }

      // precondition: 
      // postcondition: 
      // This method will determine the ratio of modification when the proficiency level increase
      private double determineRatio() {
         int randomNum = randomGenerator.Next(1,100);
         double resultModifier;
         if (randomNum < failureThreshold) {
            resultModifier = 0;
         } else if (randomNum < partialQuantityThreshold) {
            resultModifier = partialModifier;
         } else if (randomNum > expectedQuantityThreshold) {
            resultModifier = 1;
         } else {
            resultModifier = extraModifier;
         }
         return resultModifier;
      }

 
      // precondition: state must be active
      // postcondition: The output name and quantities might be different from the input.
      // The Apply method is used to simulate outcomes from the Formula class
      // Simulating the outcome will generate the quantity and the name of the resource, with a space in between
      public string[] Apply() {
         if (!isActive) {
            throw new InvalidOperationException("The state is not active.");
         }
         double modifier = determineRatio();
         int newQuantity;
         string[] storedQuantities = new string[outputQuantities.Length];
         for (int i = 0; i < outputQuantities.Length; i++) {
            newQuantity = (int)(modifier * outputQuantities[i]);
            storedQuantities = newQuantity + " " + outputNames[i];
         }
         return storedQuantities;
      }
   }
}

// Implementation invariant:
// The client is able to check the current state.
// Parsing is done in the constructor for simplicity.
// A random number generator is used for possible outcomes.
// There's no threshold for extra modifier since it will be capped by 100.