//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class SYS_PERMISSION
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public Nullable<int> IsAvailable { get; set; }
    
        public virtual SYS_MODULE SYS_MODULE { get; set; }
    }
}
