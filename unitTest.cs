using NUnit.Framework;
using PA1;
using System;
using System.Reflection;

nsmaespace PA1.Tests {

   [TestFixture]
   public class FormulaTests {

      [Tests]
      public void TestConstructor_WithValidInputAndOutput() {
         string[] validInput = {"1 IngridientA", "2 IngredientB"};
         string[] validOutput = {"3 ResultX", "4 ResultY"};

         Formula formula = new Formula(validInput, validOutput);
         Assert.IsNotNull(formula);
      }

      [Tests]
      public void TestSetProficiencyLevel_WithinRange() {
         Formula formula = new Formula(new string[] {"1 IngredientA"}, new string[] {"2 ResultX"});
         formula.SetProficiencyLevel(1);
         int proficiencyLevel = formula.GetProficiencyLevel();
         // Assert the proficiencylevel to be 1
         Assert.AreEqual(1, proficiencyLevel);
      }

      [Tests]
      public void TestSetProficiencyLevel_OutOfRange() {
         Formula formula = new Formula(new string[] {"1 IngredientA"}, new string[] {"2 ResultX"});
         Assert.Throws<ArgumentOutOfRangeException>(() => formula.SetProficiencyLevel(3));
      }

      [Tests]
      public void TestGetInputNames() {
         string[] validInput = {"1 IngredientA", "2 IngredientB"};
         Formula formula = new Formula(validInput, new string[] {"3 ResultX"});
         string[] inputNames = formula.getInputNames();

         // Assert
         Assert.IsNotNull(inputNames);
         Assert.AreEqual(2, inputNames.Length);
         Assert.AreEqual("IngredientA", inputNames[0]);
         Assert.AreEqual("IngredientB", inputNames[1]);
      }

      [Tests]
      public void TestGetOutputNames() {
         string[] validInput = {"1 IngredientA"};
         string[] validOutput = {"2 ResultX", "3 ResultY"};
         Formula formula = new Formula(validInput, validOutput);

         string[] outputNames = formula.getOutputNames();

         //Assert
         Assert.IsNotNull(outputNames);
         Assert.AreEqual(2, outputNames.Length);
         Assert.AreEqual("ResultX", outputNames[0]);
         Assert.AreEqual("ResultY", outputNames[1]);
      }

      [Tests]
      public void TestGetInputQuantities() {
         string[] validInput = {"1 IngredientA", "2 IngredientB"};
         Formula formula = new Formula(validInput, new string[] {"3 ResultX"});
         int[] inputQuantities = formula.getInputQuantities();

         //Assert
         Assert.IsNotNull(inputQuantities);
         Assert.AreEqual(2, inputQuantities.Length);
         Assert.AreEqual(1, inputQuantities[0]);
         Assert.AreEqual(2, inputQuantities[1]);
      }

      [Tests]
      public void TestGetOutputQuantities() {
         string[] validInput = {"1 IngredientA"};
         string[] validOutput = {"2 ResultX", "3 ResultY"};
         Formula formula = new Formula(validInput, validOutput);
         int[] outputQuantities = formula.getOutputQuantites();

         // Assert
         Assert.IsNotNull(outputQuantities);
         Assert.AreEqual(2, outputQuantities.Length);
         Assert.AreEqual(2, outputQuantities[0]);
         Assert.AreEqual(3, outputQuantities[1]);
      }

      [Tests]
      public void TestApply() {
         string[] validInput = {"1 IngredientA", "2 IngredientB"};
         string[] validOutput = {"3 ResultX", "4 ResultY"};
         Formula formula = new Formula(validInput, validOutput);
         formula.SetProficiencyLevel(1);

         string[] result = formula.apply();

         // Assert
         Assert.IsNotNull(result);
         Assert.AreEqual(2, result.Length);
         Assert.AreEqual("3 ResultX", result[0]);
         Assert.AreEqual("4 ResultY", result[1]);
      }

      [Test]
      public void TestDetermineRatio() {
         Formula formula = new Formula(new string[] { "1 IngredientA" }, new string[] { "2 ResultX" });
         MethodInfo determineRatioMethod = typeof(Formula).GetMethod("determineRatio", BindingFlags.NonPublic | BindingFlags.Instance);
         
         determineRatioMethod.Invoke(formula, null);

         double resultModifier = (double)determineRatioMethod.Invoke(formula, null);

         // Assert
         Assert.IsTrue(resultModifier >= 0.0 && resultModifier <= 1.0);
      }

   }
}