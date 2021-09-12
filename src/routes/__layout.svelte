<script lang="typescript">
	import {
		Column,
		Content,
		Grid,
		Header,
		Row,
		SideNav,
		SideNavItems,
		SideNavLink,
		SkipToContent
	} from 'carbon-components-svelte';
	import "carbon-components-svelte/css/g90.css";
	let isSideNavOpen = false;
</script>

<Header company="StarLog" bind:isSideNavOpen persistentHamburgerMenu={true}>
	<div slot="skip-to-content">
		<SkipToContent />
	</div>
</Header>

<SideNav bind:isOpen={isSideNavOpen}>
	<SideNavItems>
		<SideNavLink text="Home" href="/"></SideNavLink>
		<SideNavLink text="Observations" href="/observations"></SideNavLink>
		<SideNavLink text="About" href="/about"></SideNavLink>
	</SideNavItems>
</SideNav>

<Content>
	<Grid>
		<Row>
			<Column>
				<slot></slot>
			</Column>
		</Row>
	</Grid>
</Content>