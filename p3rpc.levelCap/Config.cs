using p3rpc.levelCap.Template.Configuration;
using System.ComponentModel;

namespace p3rpc.levelCap.Configuration;
public class Config : Configurable<Config>
{
    [DisplayName("Cap 1")]
    [Description("The level cap for the full moon on May 9th")]
    [DefaultValue(8)]
    public int Cap1 { get; set; } = 8;

    [DisplayName("Cap 2")]
    [Description("The level cap for the full moon on June 8th")]
    [DefaultValue(15)]
    public int Cap2 { get; set; } = 15;

    [DisplayName("Cap 3")]
    [Description("The level cap for the full moon on July 7th")]
    [DefaultValue(21)]
    public int Cap3 { get; set; } = 21;

    [DisplayName("Cap 4")]
    [Description("The level cap for the full moon on August 6th")]
    [DefaultValue(32)]
    public int Cap4 { get; set; } = 32;

    [DisplayName("Cap 5")]
    [Description("The level cap for the full moon on September 5th")]
    [DefaultValue(40)]
    public int Cap5 { get; set; } = 40;

    [DisplayName("Cap 6")]
    [Description("The level cap for the full moon on October 4th")]
    [DefaultValue(46)]
    public int Cap6 { get; set; } = 46;

    [DisplayName("Cap 7")]
    [Description("The level cap for the full moon on November 3rd")]
    [DefaultValue(53)]
    public int Cap7 { get; set; } = 53;

    [DisplayName("Cap 8")]
    [Description("The level cap for the full moon on December 2nd")]
    [DefaultValue(60)]
    public int Cap8 { get; set; } = 60;

    [DisplayName("Cap 9")]
    [Description("The level cap for the full moon on December 31st")]
    [DefaultValue(65)]
    public int Cap9 { get; set; } = 65;

    [DisplayName("Cap 10")]
    [Description("The level cap for the full moon on January 31st")]
    [DefaultValue(74)]
    public int Cap10 { get; set; } = 74;

    [DisplayName("Debug Mode")]
    [Description("Logs additional information to the console that is useful for debugging.")]
    [DefaultValue(false)]
    public bool DebugEnabled { get; set; } = false;
}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}