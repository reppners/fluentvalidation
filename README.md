Fluent Validation Library
=========================

*A Fluent API for Input, State amd Assumption Validation.*

The binary is available on [NuGet][3].

Based on the [Validation][1] library by Andrew Arnott, and a [Blog][2] by Rick Brewster.

All validation begins with the Validate object.  From there, you are able to validate input, state, and make assumptions.  Here are some examples.

Argument Validation
-------------------

    static void ComplexFunction(
        string cannotBeEmptyStr, 
        string cannotBeNullOrEmptyStr,
        int mustBeGreaterThanZero, 
        double? mustBeLessThanZeroOrNull, 
        string mustBeAllCaps)
        {
            Validate.Argument(cannotBeEmptyStr, "cannotBeEmptyStr")
                        .IsNotEmpty()
                        .Check()
                    .Argument(cannotBeNullOrEmptyStr, "cannotBeNullOrEmptyStr")
                        .IsNotNull()
                        .IsNotEmpty()
                        .Check()
                    .Argument(mustBeGreaterThanZero, "mustBeGreaterThanZero")
                        .IsInRange(v => v > 0)
                        .Check()
                    .Argument(mustBeLessThanZeroOrNull, "mustBeLessThanZeroOrNull")
                        .IsInRange(v => v < 0)
                        .Or()
                        .IsNull()
                        .Check()
                    .Argument(mustBeAllCaps, "mustBeAllCaps")
                        .That(s => s.ToUpper() == s, "Value must be all caps")
                        .Check();
        }
        
State Validation
----------------

    //Operations
    var someInstance = new SomeObject();
    Validate.State(someInstance).Operation(o => o.SomeStateFlag == true).Check();

    //Using an IDisposedObservable 
    IDisposedObservable disposableInstance = new SomeDisposableObject();
    Validate.State(disposableInstance).IsNotDisposed().Check();
    
    //Using a standard IDisposable
    IDisposable disposableInstance = new SomeDisposableObject();
    Validate.State(disposableInstance).IsNotDisposed(o => o.SomeDisposedFlag == false).Check();
    
Assumptions
-----------

    Validate.Assumptions()
        .IsTrue(() => true)
        .IsFalse(() => false)
        .IsNull(() => (string)null)
        .IsNotNull(() => "")
        .IsNull(() => (int?)null) //Works for Nullables as well
        .IsNotNull(() => (int?)5)
        .IsNotNullOrEmpty("d")
        .IsNotNullOrEmpty(new int[] { 1, 2, 3 })
        .Is<string>(someStringInstance)
        .IsServicePresent(someServiceInstance)
        .Check();
        
Some Trivia
===========

Since certain validations (such as argument and state validations) require a validation object to hold the context information of the check, we use an object pool to minimize the creation of validation objects.  While this produces a slight initial overhead, it won't matter if you make ten or ten million validations, the same few objects are used.

At most, only one small state object is created when checks do not fail.  This means that a mimial footprint is required unless exceptions are actually thrown.  For assumptions, there is no footprint at all unless a check fails.

Checks are optimized. When you call Check(), once an actual check fails, the rest of the checks are ignored.


[1]: https://github.com/AArnott/Validation "Validation"
[2]: http://blog.getpaint.net/2008/12/06/a-fluent-approach-to-c-parameter-validation/ "Paint.NET Blog"
[3]: https://www.nuget.org/packages/FluentValidationNA/ "NuGet - Fluent Validation Library"
