Feature: GetByExternalId

Scenario: Get existing by external id
	Given There is one customer in the system with external id ='external123' and id = 'internal123'
	When I request the customer with external id = 'external123'
	Then The result should include the customer with id = 'internal123'
	And The status code should be 200

Scenario: Get non existing by id
	When I request the customer with external id = 'blah.blah'
	Then The result should be empty
	And The status code should be 404
