﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forge.OpenAI.Models.ChatCompletions
{

    public class ChatCompletionResponse : ChatCompletionResponseBase
    {

        [JsonPropertyName("choices")]
        public List<ChatChoice> Choices { get; set; } = new List<ChatChoice>();

    }

}
