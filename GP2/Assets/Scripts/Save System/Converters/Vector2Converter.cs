﻿// Created by Martin M
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace SaveSystem.Converters
{
	public class Vector2Converter : JsonConverter<Vector2>
	{
		public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("x");
			writer.WriteValue(value.x);
			writer.WritePropertyName("y");
			writer.WriteValue(value.y);
			writer.WriteEndObject();
		}

		/// <inheritdoc />
		public override Vector2 ReadJson(JsonReader reader, 
			Type objectType, 
			Vector2 existingValue,
			bool hasExistingValue,
			JsonSerializer serializer)
		{
			JObject obj = JObject.Load(reader);
			return new Vector3(
				obj.Value<float>("x"), 
				obj.Value<float>("y")
			);
		}
	}
}