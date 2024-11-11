using Ludiq;

namespace Bolt
{
    [Descriptor(typeof(CreateStruct))]
    public class CreateStructDescriptor : UnitDescriptor<CreateStruct>
    {
        public CreateStructDescriptor(CreateStruct unit) : base(unit) { }

        protected override string DefinedTitle()
        {
            if (LudiqCore.Configuration.humanNaming)
            {
                return $"Create {unit.type.HumanName()}";
            }
            else
            {
                return $"new {unit.type.CSharpName()}";
            }
        }

        protected override string DefinedShortTitle()
        {
            return LudiqCore.Configuration.humanNaming ? "Create" : "new";
        }

        protected override string DefinedSurtitle()
        {
            return unit.type.DisplayName();
        }

        protected override string DefinedSummary()
        {
            return unit.type.Summary();
        }

        protected override EditorTexture DefinedIcon()
        {
            return unit.type.Icon();
        }
    }
}