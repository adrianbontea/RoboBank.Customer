Feature: GetById

Scenario: Get existing by id
	Given There is one customer in the system with id = 'internal123'
	When I request the customer with id = 'internal123'
	Then The result should include the customer with id = 'internal123'
	And The status code should be 200

Scenario: Get non existing by id
	When I request the customer with id = 'blah.blah'
	Then The result should be empty
	And The status code should be 404
