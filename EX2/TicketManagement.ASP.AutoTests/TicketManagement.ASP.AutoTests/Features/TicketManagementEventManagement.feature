Feature: TicketManagementEventManagement
	As a manager
	I want to be able to manage events: create, delete, update
	So I can do it from event management page

Scenario: Create event
	Given Manager is on event management page

	When Manager selects layout
	And Manager enter the event name "TestEvent"
	And Manager enter the event description "TestEvent"
	And Manager enter the event date and time "2019-08-01T10:10"
	And Manager create event
	Then Manager can see event named "TestEvent"

Scenario: Create event failed
	Given Manager is on event management page

	When Manager selects layout
	And Manager enter the event name "TestEvent2"
	And Manager create event
	Then Manager can not see event named "TestEvent2"

Scenario: Delete event
	Given Manager is on event management page

	When Manager selects layout
	And Manager enter the event name "TestEvent"
	And Manager enter the event description "TestEvent"
	And Manager enter the event date and time "2023-08-01T10:10"
	And Manager create event
	And Manager delete selected event with name "TestEvent"
	Then Manager can not see event named "TestEvent"

Scenario: Delete event failed
	Given User is on login page
	When User buy the first free seat

	Given Manager is on event management page
	When Manager selects layout
	And Manager delete selected event with name "Name"
	Then Manager can see event named "Name"


Scenario: Update event
	Given Manager is on event management page

	When Manager selects layout
	And Manager enter the event name "R"
	And Manager enter the event description "R"
	And Manager enter the event date and time "2020-08-01T10:10"
	And Manager create event
	And Manager selects layout
	And Manager edits "R"
	And Manager change name to "R2"
	And Manager change date to "2021-08-01T10:10"
	And Manager save changes
	Then Manager can see event named "R2"

Scenario: Update event failed
	
	Given User is on login page
	When User buy the first free seat

	Given Manager is on event management page
	When Manager selects layout
	Then Manager can not see event edit button "Name"