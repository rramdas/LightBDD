﻿using System.Collections.Generic;

namespace LightBDD.ParallelizableExample.Domain
{
	public class ContactBook
	{
		private readonly IDictionary<string, Contact> _contacts = new Dictionary<string, Contact>();

		public void AddContact(string name, string phone)
		{
			_contacts.Add(name, new Contact(name, phone));
		}

		public void Remove(string name)
		{
			_contacts.Remove(name);
		}

		public void Clear()
		{
			_contacts.Clear();
		}

		public IEnumerable<Contact> Contacts { get { return _contacts.Values; } }
	}
}
