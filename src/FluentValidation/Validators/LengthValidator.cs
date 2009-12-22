#region License
// Copyright 2008-2009 Jeremy Skinner (http://www.jeremyskinner.co.uk)
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://www.codeplex.com/FluentValidation
#endregion

namespace FluentValidation.Validators {
	using System;
	using Attributes;
	using Resources;

	[ValidationMessage(Key=DefaultResourceManager.LengthValidatorError)]
	public class LengthValidator : PropertyValidator, ILengthValidator {
		public int Min { get; private set; }
		public int Max { get; private set; }

		public LengthValidator(int min, int max) {
			Max = max;
			Min = min;

			if (max < min) {
				throw new ArgumentOutOfRangeException("max", "Max should be larger than min.");
			}
		}

		protected override bool IsValid(PropertyValidatorContext context) {
			int length = context.PropertyValue == null ? 0 : context.PropertyValue.ToString().Length;

			if (length < Min || length > Max) {
				context.MessageFormatter
					.AppendArgument("MinLength", Min)
					.AppendArgument("MaxLength", Max)
					.AppendArgument("TotalLength", length);

				return false;
			}

			return true;
		}
	}

	[ValidationMessage(Key = DefaultResourceManager.ExactLengthValidatorError)]
	public class ExactLengthValidator<TInstance> : LengthValidator {
		public ExactLengthValidator(int length) : base(length,length) {
			
		}
	}

	public interface ILengthValidator : IPropertyValidator {
		int Min { get; }
		int Max { get; }
	}
}