namespace RecipeHub.Common
{
    public static class ValidationConstants
    {
        public const int RecipeNameMinLength = 3;
        public const int RecipeNameMaxLength = 50;

        public const int IngredientNameMinLength = 2;
        public const int IngredientNameMaxLength = 40;

        public const int WeightMin = 1;
        public const int WeightMax = 1000;

        public const int StepNameMinLength = 10;
        public const int StepNameMaxLength = 200;
    }
}
