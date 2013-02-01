using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Utility.Attachment
{
    /// <summary>
    /// 附件Provider配置项处理类
    /// </summary>
    public class AttachmentProviderConfigurationSectionHandler : ConfigurationSection
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AttachmentProviderConfigurationSectionHandler()
        {
            m_defaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), null);
            m_providers = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null);
            m_properties = new ConfigurationPropertyCollection();

            m_properties.Add(m_providers);
            m_properties.Add(m_defaultProvider);
        }

        private readonly ConfigurationProperty m_defaultProvider;
        private readonly ConfigurationProperty m_providers;
        private readonly ConfigurationPropertyCollection m_properties;

        /// <summary>
        /// 默认Provider
        /// </summary>
        [ConfigurationProperty("defaultProvider")]
        public string DefaultProvider
        {
            get { return (string)base[m_defaultProvider]; }
            set { base[m_defaultProvider] = value; }
        }

        /// <summary>
        /// 当前Provider
        /// </summary>
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base[m_providers]; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get { return m_properties; }
        }
    }
}
