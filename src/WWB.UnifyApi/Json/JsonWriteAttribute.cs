using System;

namespace WWB.UnifyApi.Json
{
    public class JsonAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 属性是否可以被序列化
        /// </summary>
        public bool Readable { get; set; } 
        
        /// <summary>
        /// 属性是否可以被反序列化
        /// </summary>
        public bool Writable { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public object? DefaultValue { get; set; }
    
        public JsonAttribute()
        {
            this.Readable = true;
            this.Writable = true;
        }
    }

}