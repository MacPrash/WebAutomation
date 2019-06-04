Feature: GlassWeb
	Checking the basic settings

Scenario: Test the country filter
	Given user is on the landing page for WD site on "chrome"
		And the country filter is available
	When user selects "Belgium" from the Country filter list on left panel
	And clicks on Update button for the country filter list
	Then the grid display all meetings that are associated with the country "Belgium"
		And no meetings associated with any other country appears on the list

Scenario: Test the company name filter
	Given user is on the landing page for WD site on "chrome"
	When user clicks the Company Name "Advantech" hyperlink
	Then the user land onto the "Advantech" vote card page
		And "Advantech" should appear in the top banner


	
