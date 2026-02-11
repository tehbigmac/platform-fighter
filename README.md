INITIAL:
cd git
cd platform-fighter

PULLING:
run git pull
if it aborts, run git clean -fd and try again

COMMITTING:
git add .
git commit -am "comment"
git push

BRANCHING:
git checkout -b <branch name>

MERGING:
git checkout main
git pull origin main
git push
