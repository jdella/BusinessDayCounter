# BusinessDayCounter

## General Comments
 - I took the approach that the intention of this would be a reusable utility library/module rather than a self-contained app.
- I took a TDD-approach, writing failing unit tests first, then filling code to pass them.  Most of the unit tests are based on the provided test cases however there is some additional tests for supporting logic.
- I worked through the tasks sequentially, imagining a sequence of iterative improvements/change requirements.
- In code comments may be a little more liberal than usual.
- I avoided Googling in the spirit of the excercise, there may be more efficient/better ways to do certaint things.  In a real-world project, this might otherwise be part of the process.
- I probably had more fun that I should have on this! 😊

## Decision Register
- TDD approach
- C# (requirement)
- .Net 8 (latest version, in support).
  - *Note: No top level statements - personal preference, can be confusing for some (I like structure 😊 )
- xUnit + FluentAssertions - personal preference, fluent assertions allows for descriptive asserts which I like

## Considerations/Assumptions
-	Time of day/Timezones -	The fact that first/second date aren’t included in the count mitigates this somewhat, although timezone conversions could affect the date if they had to be considered
    -	**Assumption**: this isn’t relevant so was not necessarily considered/factored in to any great degree
-	Project intention
    -	**Assumption**: As mentioned above, assumed intention is for a reusable utility library/module

## Structure
Pretty Simple

Projects:
- Tests - self-explanatory
- Logic - core business logic
- Contracts - interface and models that would be required to consume the BusinessDayCounter functions.  Mostly a logical abstraction which would help a consumer and enforce contracts

Folders - generally arranged by business function.  Didn't go overboard.  Some classes sit in project root which may not be a good idea on larger projects.

## Improvements/Further work
 - Not mentioned in the spec but there is edge case logic flaw that would occur when two successive public holiday dates (which move to Monday if falling on a weekend) both fall on a weekend.  For example, if Christmas and Boxing Day fell on Sat/Sun respectively, currently both return the Monday after as their Public Holiday date.  It would also occur if Christmas fell on Sunday.
   - Side Note: No unit tests were written for this (yet) but its suspected that the logic may still return the correct count in *most* cases although the actual date of PH would be incorrect for one of them
  


