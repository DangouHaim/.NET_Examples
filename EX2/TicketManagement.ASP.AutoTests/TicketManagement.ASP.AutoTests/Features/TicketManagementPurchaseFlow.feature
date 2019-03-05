Feature: TicketManagementPurchaseFlow
	As a user
	I want to be able to buy tickets and see purchase history
	So I can do it from login page, events home page and purchase history page

Scenario: Buy the ticket
	Given User is on login page

	When User buy the first free seat
	Then Purchase history contains any seats

Scenario: Buy the ticket failed
	Given User without balance is on login page

	When User buy the first free seat
	Then Purchase history not contains any seats