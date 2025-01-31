using System;
using UnityEngine;

namespace Journal.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class JournalVisibleAttribute : PropertyAttribute
	{
		public JournalItemType JournalItemType { get; }
		
		public string DataProperty { get; set; }
		
		public JournalVisibleAttribute(string dataProperty, JournalItemType journalItemType)
		{
			JournalItemType = journalItemType;
			DataProperty = dataProperty;
		}
	}
}