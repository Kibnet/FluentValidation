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
	using Internal;
	using Resources;
	using Results;

	[ValidationMessage(Key = DefaultResourceManager.InclusiveBetweenValidatorError)]
	public class InclusiveBetweenValidator : PropertyValidator, IBetweenValidator {
		public InclusiveBetweenValidator(IComparable from, IComparable to) {
			To = to;
			From = from;

			if (to.CompareTo(from) == -1) {
				throw new ArgumentOutOfRangeException("to", "To should be larger than from.");
			}
		}

		public IComparable From { get; private set; }
		public IComparable To { get; private set; }

		protected override bool IsValid(PropertyValidatorContext context) {
			var propertyValue = (IComparable)context.PropertyValue;

			if (propertyValue.CompareTo(From) < 0 || propertyValue.CompareTo(To) > 0) {

				context.MessageFormatter
					.AppendArgument("From", From)
					.AppendArgument("To", To)
					.AppendArgument("Value", context.PropertyValue);

				return false;
			}
			return true;
		}
	}

	public interface IBetweenValidator : IPropertyValidator {
		IComparable From { get; }
		IComparable To { get; }
	}
}