Orchard-RoleCommands
=

Orchard CMS Role Commands
-

**role list** *[/WithFeature:"feature"] [/WithPermission:permission] [/IncludeUsers:true|false] [/IncludePermissions:true|false]*  
Lists all roles by name

**role detail** &lt;name&gt; *[/WithFeature:"feature"] [/WithPermission:permission] [/IncludeUsers:true|false] [/IncludePermissions:true|false]*  
Displays Role Details

**permission list** *[/WithFeature:"feature"]*  
Lists Permissions

**user roles** &lt;username&gt;  
Lists a User's Roles

**user add role** &lt;username&gt; &lt;role&gt;  
Adds a User to a Role

**user remove role** &lt;username&gt; &lt;role&gt;  
Removes a User from a Role

**role add permission** &lt;role&gt; &lt;permission&gt;  
Adds a Permission to a Role

**role remove permission** &lt;role&gt; &lt;permission&gt;  
Removes a Permission to a Role

**role create** &lt;role&gt;  
Creates a Role

**role delete** &lt;role&gt;  
Deletes a Role

Orchard RolePermissions Export &amp; Recipe Handler
-

This module also adds a custom Export for every Role.  
It also adds  a Recipe handler that for the creation of new Roles, as allows adding/removal of permissions.  

*Example:*

	<Orchard>
	  <Recipe>
		<Name>Test Recipe</Name>
		<Author>admin</Author>
	  </Recipe>
	  <RolePermissions>
		<Anonymous>
		  <Remove>AddComment</Remove>
		  <Add>Submit_CustomFormSubmission</Add>
		</Anonymous>
		<SomeNewRole>
		  <Add>ViewContent</Add>
		</SomeNewRole>
	  </RolePermissions>
	</Orchard>