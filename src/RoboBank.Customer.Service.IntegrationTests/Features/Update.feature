Feature: Update

Scenario: Update existing
	Given There is one customer in the system with id = 'internal123'
	When I send a customer update with id = 'internal123' and external id = 'external123'
	Then The status code should be 200
	And The customer with id = 'internal123' should be updated with external id = 'external123'

Scenario: Update non existing
	When I send a customer update with id = 'internal123' and external id = 'external123'
	Then The status code should be 200
	And Customer with id = 'internal123' should not be created

Scenario: Update without id
	Given There is one customer in the system with id = 'internal123'
	When I send a customer update with external id = 'external123'
	Then The status code should be 500
