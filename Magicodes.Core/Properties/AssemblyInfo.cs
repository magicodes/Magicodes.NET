using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web;

// 有关程序集的常规信息通过以下
// 特性集控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("Magicodes.Core")]
[assembly: AssemblyDescription("Magicodes.NET核心代码框架库，具体请关注http://www.magicodes.net/")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Magicodes.NET团队")]
[assembly: AssemblyProduct("Magicodes.NET")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。  如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("cb1dc697-5648-47c8-aca0-3c29946849fd")]

// 程序集的版本信息由下面四个值组成: 
//
//      主版本
//      次版本 
//      生成号
//      修订号
//
// 可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值，
// 方法是按如下所示使用“*”: 
// [assembly: AssemblyVersion("1.0.*")]
//必须移除AssemblyFileVersion特性设置，自动版本号才会生效
[assembly: AssemblyVersion("1.1.*")]
[assembly: PreApplicationStartMethod(typeof(Magicodes.Core.Starter), "Start")]
