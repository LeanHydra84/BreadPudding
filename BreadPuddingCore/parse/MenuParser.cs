using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace BreadPudding
{

    public delegate bool PredicateFood(Food food);
    public delegate bool PredicateSection(KitchenSection section);
    public delegate bool PredicateMenu(SubMenu menu);

    public enum FoodData
    {
        Name =          0b001,
        Description =   0b010,
        Ingredients =   0b100,
        All =           0b111,
    }

    public class MenuParser
    {

        private Func<Food, bool> FoodChecks;
        private Func<KitchenSection, bool> SectionChecks;
        private Func<SubMenu, bool> MenuChecks;

        public MenuParser()
        {
            // This feels messy and gross
            FoodChecks = delegate { return true; };
            SectionChecks = delegate { return true; };
            MenuChecks = delegate { return true; };
        }

        public void AddFoodRule(Func<Food, bool> predicate) =>
            FoodChecks += predicate;

        public void AddSectionRule(Func<KitchenSection, bool> predicate) =>
            SectionChecks += predicate;

        public void AddMenuRule(Func<SubMenu, bool> predicate) =>
            MenuChecks += predicate;

        public void ClearFoodRule() => FoodChecks = a => true;
        public void ClearSectionRule() => SectionChecks = a => true;
        public void ClearMenuRule() => MenuChecks = a => true;

        private bool CheckRule<T>(T? f, Func<T, bool> checklist)
        {
            if (f == null) return false;
            foreach(Func<T, bool> check in checklist.GetInvocationList())
            {
                bool val = check(f);
                if (!val) return false;
            }
            return true;
        }

        public KitchenSection[] ParseSections(SubMenu menu)
        {
            if (menu.Items == null) return new KitchenSection[0];
            return menu.Items.Where(a => CheckRule(a, SectionChecks)).Cast<KitchenSection>().ToArray();
        }

        public Food[] ParseFood(IEnumerable<Food?> block, FoodData option)
        {
            Food[] parsed = block.Where(a => CheckRule(a, FoodChecks)).Cast<Food>().ToArray();
            if (option == FoodData.All) return parsed;

            foreach (Food f in parsed)
            {
                if ((option & FoodData.Name) == 0) f.Name = null;
                if ((option & FoodData.Description) == 0) f.Description = null;
                if ((option & FoodData.Ingredients) == 0) f.Ingredients = null;
            }

            return parsed;
        }

        public Food[] ParseFood(KitchenSection section, FoodData option)
        {
            if (section.Food == null) return new Food[0];
            return ParseFood(section.Food, option);
        }

        public Food[] ParseMenu(SubMenu menu, FoodData option = FoodData.All)
        {
            var parsedSections = ParseSections(menu);
            List<Food> foods = new();
            foreach (var sec in parsedSections)
            {
                Food[] foodSet = ParseFood(sec, option);
                foods.AddRange(foodSet);
            }

            return foods.ToArray();
        }

        public Food[] ParseMenus(IEnumerable<SubMenu> menus, FoodData option = FoodData.All)
        {
            List<Food> foods = new();
            foreach (SubMenu menu in menus)
            {
                if (!CheckRule(menu, MenuChecks)) continue;
                foods.AddRange(ParseMenu(menu, option));
            }
            return foods.ToArray();
        }

        

        public string[] RawDataConvert(Food[] food)
        {
            return food.Select(a => a.Name).Where(a => a != null).Cast<string>().ToArray();
        }

    }
}