# Impenetrable (Working title) #

A game about penetration.

## Using Git Flow ##

We'll be using the [Git Flow](https://www.atlassian.com/git/tutorials/comparing-workflows/gitflow-workflow) approach to repo management.

To update your local repo, use `git pull`.

Before making any commits, please make sure your git client is configured with your name and Bitbucket email so it's easier to track who submitted what. To do this, run the following commands once you've pulled the repo:

`git config user.email "emailhere"`

`git config user.name "Your Name"`

### Branches ###

We will use 2 main 'branches' (think of these as sub-versions of the code), instead of a single one:

* `master`: This branch now only contains our release builds - everything in here will have a version number, has gone through rigorous testing, and is the latest "official" version of the game.
* `dev`: This branch contains the current, functioning, up-to-date code. This is basically what `master` used to be.

You'll probably still be on the `master` branch in your repo. Swap to the `dev` branch with `git checkout dev`.

In addition to this, we'll be using three other 'groups' of branches: features, releases, and hotfixes.

### Adding Features ###

When you want to make a new feature, first decide on a descriptive name for it (eg: `my-feature-name`). This can't contain spaces, and should be lower case. It also can't begin with `release-` or `hotfix-`.

To begin, run the command `git checkout -b my-feature-name` (replacing `my-feature-name` with your chosen feature name). This will create a new branch with the name you gave based on the branch you were just in, and it'll swap you over to it. It's a good idea to make sure you're in the `dev` branch beforehand.

You can now update things as normal, using `git add *` and `git commit -am "Changes here"` to save changes.

Whenever you want to update the online version, use `git push -u origin my-feature-name`. This should automatically create the remote branch, push your commits to it, and link the branches for future updates.

### Merging Features ###

Throughout all of this, the `dev` branch has remained unaffected by your new feature. This is good, because we can keep a functioning copy of the code whilst everyone can experiment on their own things.

When you're happy with the feature, it can be merged with the `dev` branch. Normally this can be done manually, but as this is a new system and it's in everyone's interests to avoid issues, I've locked down the `dev` branch so nobody can manually make changes to it until we've all got a better understanding of this.

To add your feature to the live code, make a pull request (a request to merge data, stupid name if you ask me) using the link on the left, select your branch, and the dev branch. Explain what's changed in the box below, tick "close branch-name after the pull request is merged" if your feature is completed, and submit it.

This will now show up here on Bitbucket (it's not a native git thing), it can be examined, checked for suitability and compatability with features that may have been added in the meantime, and then merged.

### Releases ###

Once we've got a good collection of features added and we're ready to release it, we can create a new branch based off `dev`. This branch will be formatted `release-x.y.0`, where `x` is the major version (0 at the moment), and `y` is one larger than the previous release - this forms the basis for version numbering. We can now test the release build, trying to find bugs or problems. If any are found, they can be fixed on the branch and the change merged back into `dev` (to keep it up-to-date).

`dev` should **never** be merged into a `release-` branch once it's been created. This lets us keep on making new features, without having to worry about the version currently being tested.

Once we're happy with a release build, it can finally be merged into `master`. This way, `master` *only* ever contains well-tested, functioning versions of the game.

### Hotfixes ###

If there's a problem with the game that can't wait until the next release (major bug, exploit, etc.) then we can branch off master into a `hotfix-x.y.z` branch, where x.y is the current version, and z is one larger than the patch version for the current build (0 to begin with).

Here, we fix the problem, test it to make sure it works, then merge it back into `master`.

We then merge `master` with `dev` so that we don't have to fix the same bug twice.


### Summary ###

* `master` for the latest stable version
* `dev` for the current code
* `release-*` for in-testing release builds, based off `dev`
* `hotfix-*` for in-testing fixes, based off `master`
* Any other branch name for trying out new features
* `git checkout branch-name` to swap branches
* `git checkout -b branch-name` to make a new branch based on your current one
* `git push -u origin branch-name` to upload your current branch
* Use the Pull Request page to merge with the `dev` branch