Feature: Search

Scenario: Search Existing That Allows Search
	Given There is one customer in the system with profile Something = 'one' and SometingElse = 'two' and AllowsSearch = 'True' and id = 'internal123'
	When I search for Something = 'one' and SometingElse = 'two'
	Then The list result should include the customer with id = 'internal123'
	And The status code should be 200

Scenario: Search Existing That Does Not Allow Search
	Given There is one customer in the system with profile Something = 'one' and SometingElse = 'two' and AllowsSearch = 'False' and id = 'internal123'
	When I search for Something = 'one' and SometingElse = 'two'
	Then The list result should be empty
	And The status code should be 200

Scenario: Search non existing
	Given There is one customer in the system with profile Something = 'one' and SometingElse = 'two' and AllowsSearch = 'True' and id = 'internal123'
	When I search for Something = 'blah' and SometingElse = 'blah'
	Then The list result should be empty
	And The status code should be 200
