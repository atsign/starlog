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
	import ObservationList from './ObservationList.svelte';

	let isSideNavOpen = true;
</script>

<Header company="StarLog" bind:isSideNavOpen persistentHamburgerMenu={true}>
	<div slot="skip-to-content">
		<SkipToContent />
	</div>
</Header>

<SideNav bind:isOpen={isSideNavOpen}>
	<SideNavItems>
		<SideNavLink text="Link 1"></SideNavLink>
		<SideNavLink text="Link 2"></SideNavLink>
		<SideNavLink text="Link 3"></SideNavLink>
	</SideNavItems>
</SideNav>

<Content>
	<Grid>
		<Row>
			<Column>
				<ObservationList />
			</Column>
		</Row>
	</Grid>
</Content>
